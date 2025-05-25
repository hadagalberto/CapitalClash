using MediatR;

namespace CapitalClash.Application.Events
{
    public class RentPaid : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerName { get; set; } = "";
        public string OwnerName { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public int Amount { get; set; }
        public bool PlayerBankrupt { get; set; }
    }
}
