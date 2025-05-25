using System.Collections.Concurrent;

namespace CapitalClash.Models
{

    public class GameStore
    {
        public Dictionary<string, GameRoom> Rooms { get; } = new();
    }

}
