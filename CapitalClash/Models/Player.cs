namespace CapitalClash.Models
{
    public class Player
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Nickname { get; set; } = string.Empty;
        public string ConnectionId { get; set; } = string.Empty;
        public int Balance { get; set; }
        public int ConsecutiveDoubles { get; set; } = 0;
        public int SkippedTurns { get; set; } = 0;
        public bool NextTurnDoubleMoney { get; set; } = false;
        public int Position { get; set; } = 0;

        public bool IsInJail { get; set; } = false;
        public int JailTurns { get; set; } = 0;
    }

}
