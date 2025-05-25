using MediatR;

namespace CapitalClash.Application.Events
{
    public class PropertyUpgraded : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerName { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public int NewLevel { get; set; }
    }
}
