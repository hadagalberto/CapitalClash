using CapitalClash.Domain.Cards;

namespace CapitalClash.Services
{
    public static class ChanceCardProvider
    {
        private static readonly List<IChanceCard> _cards = new()
        {
            new GainMoneyCard(),
            new LoseMoneyCard(),
            new MoveForwardCard(),
            new MoveBackwardCard(),
            new GoToJailCard(),
            new SkipNextTurnCard(),
            new GainDoubleNextTurnCard()
        };

        public static IChanceCard GetRandomCard()
        {
            var rnd = new Random();
            return _cards[rnd.Next(_cards.Count)];
        }
    }
}
