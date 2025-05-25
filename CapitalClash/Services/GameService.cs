using CapitalClash.Enums;
using CapitalClash.Models;
using CapitalClash.Services.Actions;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Services
{
    public class GameService
    {
        private readonly List<IAction> _turnPipeline;
        private readonly IMediator _mediator;

        public GameService(IMediator mediator)
        {
            _turnPipeline = new List<IAction>
            {
                new RollDiceAction(),
                new LandOnPropertyAction(),
                new EvaluateAvailableActionsAction(),
            };
            _mediator = mediator;
        }

        public async Task ExecuteTurn(GameRoom room, Player player, IHubCallerClients clients, string roomCode)
        {
            var context = new GameContext
            {
                Room = room,
                Player = player,
                Clients = clients,
                RoomCode = roomCode,
                Mediator = _mediator,
            };

            try
            {
                await new CheckJailStatusAction().ExecuteAsync(context);
            }
            catch (JailHoldException)
            {
                // jogador permanece preso, não faz nada
                var currentIndex = room.Players.FindIndex(p => p.Id == player.Id);
                var next = room.Players[(currentIndex + 1) % room.Players.Count];
                room.CurrentTurnPlayerId = next.Id;
                await clients.Group(roomCode).SendAsync("NextTurn", next.Nickname);
                return;
            }

            if (player.SkippedTurns > 0)
            {
                player.SkippedTurns--;

                await clients.Group(roomCode).SendAsync("TurnSkipped", player.Nickname);

                // Passa o turno para o próximo jogador
                var currentIndex = room.Players.FindIndex(p => p.Id == player.Id);
                var next = room.Players[(currentIndex + 1) % room.Players.Count];
                room.CurrentTurnPlayerId = next.Id;

                await clients.Group(roomCode).SendAsync("NextTurn", next.Nickname);
                return;
            }

            foreach (var action in _turnPipeline)
            {
                await action.ExecuteAsync(context);
            }

            if (context.IsDouble)
            {
                context.Player.ConsecutiveDoubles++;

                if (context.Player.ConsecutiveDoubles >= GameConfig.MaxConsecutiveDoubles)
                {
                    // Enviar jogador para a prisão
                    var jailIndex = context.Room.Board.Spaces
                        .FirstOrDefault(s => s.Type == BoardSpaceType.Jail)?.Index ?? 0;

                    context.Room.State.PlayerPositions[context.Player.Id] = jailIndex;
                    context.Player.ConsecutiveDoubles = 0;

                    await context.Clients.Group(context.RoomCode).SendAsync("SentToJail", context.Player.Nickname);
                    var currentIndex = context.Room.Players.FindIndex(p => p.Id == context.Player.Id);
                    var next = context.Room.Players[(currentIndex + 1) % context.Room.Players.Count];
                    context.Room.CurrentTurnPlayerId = next.Id;
                    await context.Clients.Group(context.RoomCode).SendAsync("NextTurn", next.Nickname);
                    return;
                }

                await context.Clients.Group(context.RoomCode).SendAsync("ExtraTurn", context.Player.Nickname);
            }
            else
            {
                context.Player.ConsecutiveDoubles = 0;

                var currentIndex = context.Room.Players.FindIndex(p => p.Id == context.Player.Id);
                var next = context.Room.Players[(currentIndex + 1) % context.Room.Players.Count];
                context.Room.CurrentTurnPlayerId = next.Id;
                await context.Clients.Group(context.RoomCode).SendAsync("NextTurn", next.Nickname);
            }

            await context.Clients.Group(context.RoomCode).SendAsync("UpdatePlayers", context.Room.Players);
        }
    }

}
