using System.Numerics;

namespace CapitalClash.Models
{
    public class GameRoom
    {
        public string RoomCode { get; set; } = Guid.NewGuid().ToString()[..6].ToUpper();
        public List<Player> Players { get; set; } = new();
        public string? CurrentTurnPlayerId { get; set; }
        public GameState State { get; set; } = new();
        public bool IsPrivate { get; set; }
        public string? Password { get; set; }
        public DateTime LastTurnTime { get; set; } = DateTime.UtcNow;
        public int TurnTimeoutSeconds { get; set; } = 30; // in seconds
        public GameBoard Board { get; set; } = GameBoard.CreateDefaultBoard();
    }

}
