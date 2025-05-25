namespace CapitalClash.Models
{
    public static class GameConfig
    {
        public static int InitialBalance { get; } = 1500;
        public static int MaxPlayers { get; } = 4;
        public static int StartBonusAmount { get; } = 200;
        public static int JailFine { get; } = 100;
        public static int ChanceLoseMoney { get; } = 80;
        public static int MaxConsecutiveDoubles { get; } = 3;
    }
}
