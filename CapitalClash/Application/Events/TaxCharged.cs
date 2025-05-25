using MediatR;

namespace CapitalClash.Application.Events
{
    public class TaxCharged : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerName { get; set; } = "";
        public int Amount { get; set; }
    }
}
