using CapitalClash.Application.Events;
using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public class GainDoubleNextTurnCard : IChanceCard
    {
        public string Name => "No próximo turno, seu saldo será dobrado!";

        public async Task ExecuteAsync(GameContext context, IMediator mediator)
        {
            context.Player.NextTurnDoubleMoney = true;

            await mediator.Publish(new ChanceResolved
            {
                RoomCode = context.RoomCode,
                PlayerName = context.Player.Nickname,
                Message = Name
            });
        }
    }
}
