using MediatR;

namespace CapitalClash.Application.Events
{
    public class ChanceTriggered : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerId { get; set; } = "";
    }
}
