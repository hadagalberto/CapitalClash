using CapitalClash.Application.Events;
using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public class LoseMoneyCard : IChanceCard
    {
        public string Name => "Perdeu dinheiro em taxas bancárias!";

        public async Task ExecuteAsync(GameContext context, IMediator mediator)
        {
            var amount = GameConfig.ChanceLoseMoney;
            context.Player.Balance = Math.Max(0, context.Player.Balance - amount);

            await mediator.Publish(new ChanceResolved
            {
                RoomCode = context.RoomCode,
                PlayerName = context.Player.Nickname,
                Message = $"{Name} - R${amount} perdidos."
            });
        }
    }
}
