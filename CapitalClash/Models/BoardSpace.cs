using CapitalClash.Enums;

namespace CapitalClash.Models
{
    public class BoardSpace
    {

        public int Index { get; set; }
        public string Name { get; set; } = "";
        public BoardSpaceType Type { get; set; }
        public int? Cost { get; set; }
        public int? Rent { get; set; }
        public string? OwnerId { get; set; }
        public int UpgradeLevel { get; set; } = 0;
        public int MaxUpgradeLevel { get; set; } = 3;

    }
}
