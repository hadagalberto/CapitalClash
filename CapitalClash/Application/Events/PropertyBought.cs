using MediatR;

namespace CapitalClash.Application.Events
{
    public class PropertyBought : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerId { get; set; } = "";
        public string PlayerName { get; set; } = "";
        public string PropertyName { get; set; } = "";
    }
}
