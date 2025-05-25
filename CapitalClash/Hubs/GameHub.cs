using CapitalClash.Application.Commands;
using CapitalClash.Enums;
using CapitalClash.Models;
using CapitalClash.Services;
using CapitalClash.Services.Actions;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Hubs
{
    public class GameHub : Hub
    {
        private readonly IMediator _mediator;

        private readonly GameStore _gameStore;

        public GameHub(IMediator mediator, GameStore gameStore)
        {
            _mediator = mediator;
            _gameStore = gameStore;
        }

        public async Task JoinRoom(string roomCode, string nickname, string password)
        {
            try
            {
                if (roomCode == null) {
                    throw new Exception("Código da sala não pode ser nulo");
                }

                if (nickname == null)
                {
                    throw new Exception("Nickname não pode ser nulo");
                }

                if (nickname.Length < 3 || nickname.Length > 20)
                {
                    throw new Exception("Nickname deve ter entre 3 e 20 caracteres");
                }

                var room = _gameStore.Rooms.GetValueOrDefault(roomCode);

                if (room == null)
                {
                    throw new Exception("Sala não encontrada");
                }

                if (room.IsPrivate && room.Password != password)
                    throw new Exception("Senha incorreta");

                var player = new Player
                {
                    Nickname = nickname,
                    ConnectionId = Context.ConnectionId,
                    Id = Guid.NewGuid().ToString(),
                    Balance = 1500,
                    SkippedTurns = 0,
                    ConsecutiveDoubles = 0,
                    NextTurnDoubleMoney = false,
                    IsInJail = false,
                    JailTurns = 0
                };

                room.Players.Add(player);
                room.State.PlayerPositions[player.Id] = 0;

                if (room.CurrentTurnPlayerId == null)
                {
                    room.CurrentTurnPlayerId = player.Id;
                    room.LastTurnTime = DateTime.UtcNow;
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
                await Clients.Group(roomCode).SendAsync("PlayerJoined", nickname);

                await Clients.Caller.SendAsync("LoadBoard", room.Board.Spaces);
                await Clients.Group(roomCode).SendAsync("UpdatePlayers", room.Players);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("JoinFailed", ex.Message);
                throw; // isso ainda faz o SignalR retornar erro ao JS
            }
        }

        public string Ping() => "pong";

        public async Task RollDice(string roomCode)
        {
            var command = new RollDiceCommand
            {
                RoomCode = roomCode,
                ConnectionId = Context.ConnectionId,
                Clients = Clients
            };

            await _mediator.Send(command);
        }

        public async Task BuyProperty(string roomCode)
        {
            await _mediator.Send(new BuyPropertyCommand
            {
                RoomCode = roomCode,
                ConnectionId = Context.ConnectionId,
                Clients = Clients
            });
        }

        public async Task UpgradeProperty(string roomCode)
        {
            await _mediator.Send(new UpgradePropertyCommand
            {
                RoomCode = roomCode,
                ConnectionId = Context.ConnectionId,
                Clients = Clients
            });
        }

        public async Task PayJailFine(string roomCode)
        {
            await _mediator.Send(new PayJailFineCommand
            {
                RoomCode = roomCode,
                ConnectionId = Context.ConnectionId,
                Clients = Clients
            });
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            foreach (var room in _gameStore.Rooms.Values)
            {
                var player = room.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
                if (player != null)
                {
                    room.Players.Remove(player);
                    room.State.PlayerPositions.Remove(player.Id);

                    if (room.CurrentTurnPlayerId == player.Id && room.Players.Any())
                    {
                        var next = room.Players.First();
                        room.CurrentTurnPlayerId = next.Id;
                        await Clients.Group(room.RoomCode).SendAsync("NextTurn", next.Nickname);
                    }

                    await Clients.Group(room.RoomCode).SendAsync("PlayerLeft", player.Nickname);
                    break;
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
