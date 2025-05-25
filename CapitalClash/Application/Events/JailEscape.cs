using MediatR;

namespace CapitalClash.Application.Events
{

    public class JailEscape : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerName { get; set; } = "";
        public string Reason { get; set; } = "";
    }
}
