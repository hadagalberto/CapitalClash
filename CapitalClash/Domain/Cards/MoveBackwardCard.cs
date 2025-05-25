using CapitalClash.Application.Events;
using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public class MoveBackwardCard : IChanceCard
    {
        public string Name => "Volte 2 casas!";

        public async Task ExecuteAsync(GameContext context, IMediator mediator)
        {
            var boardSize = context.Room.Board.Spaces.Count;
            context.Room.State.PlayerPositions[context.Player.Id] =
                (context.Room.State.PlayerPositions[context.Player.Id] - 2 + boardSize) % boardSize;

            await mediator.Publish(new ChanceResolved
            {
                RoomCode = context.RoomCode,
                PlayerName = context.Player.Nickname,
                Message = Name
            });
        }
    }

}
