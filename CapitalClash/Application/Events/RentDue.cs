using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Application.Events
{
    public class RentDue : INotification
    {
        public string RoomCode { get; set; } = "";
        public string PlayerId { get; set; } = "";
        public BoardSpace Property { get; set; } = null!;
    }
}
