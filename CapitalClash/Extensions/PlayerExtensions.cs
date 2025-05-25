using CapitalClash.Models;

namespace CapitalClash.Extensions
{
    public static class PlayerExtensions
    {
        public static int ApplyEarnings(this Player player, int amount)
        {
            if (player.NextTurnDoubleMoney)
            {
                amount *= 2;
                player.NextTurnDoubleMoney = false;
            }

            player.Balance += amount;
            return amount;
        }
    }

}
