namespace CapitalClash.Models
{
    public class GameState
    {
        public Dictionary<string, int> PlayerPositions { get; set; } = new();
        public int DiceRoll { get; set; }
    }

}
