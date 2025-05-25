using CapitalClash.Application.Events;
using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public class MoveForwardCard : IChanceCard
    {
        public string Name => "Avance 3 casas!";

        public async Task ExecuteAsync(GameContext context, IMediator mediator)
        {
            var boardSize = context.Room.Board.Spaces.Count;
            context.Room.State.PlayerPositions[context.Player.Id] =
                (context.Room.State.PlayerPositions[context.Player.Id] + 3) % boardSize;

            await mediator.Publish(new ChanceResolved
            {
                RoomCode = context.RoomCode,
                PlayerName = context.Player.Nickname,
                Message = Name
            });
        }
    }
}
