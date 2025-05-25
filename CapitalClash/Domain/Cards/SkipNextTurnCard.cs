using CapitalClash.Application.Events;
using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public class SkipNextTurnCard : IChanceCard
    {
        public string Name => "Você perderá o próximo turno!";

        public async Task ExecuteAsync(GameContext context, IMediator mediator)
        {
            context.Player.SkippedTurns++;

            await mediator.Publish(new ChanceResolved
            {
                RoomCode = context.RoomCode,
                PlayerName = context.Player.Nickname,
                Message = Name
            });
        }
    }

}
