using MediatR;

namespace CapitalClash.Application.Events
{
    public class ChanceResolved : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerName { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
