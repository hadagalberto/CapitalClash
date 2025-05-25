using CapitalClash.Application.Events;
using CapitalClash.Extensions;
using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public class GainMoneyCard : IChanceCard
    {
        public string Name => "Ganhou um sorteio misterioso!";

        public async Task ExecuteAsync(GameContext context, IMediator mediator)
        {
            var ganho = context.Player.ApplyEarnings(100);

            await mediator.Publish(new ChanceResolved
            {
                RoomCode = context.RoomCode,
                PlayerName = context.Player.Nickname,
                Message = Name
            });
        }
    }
}
