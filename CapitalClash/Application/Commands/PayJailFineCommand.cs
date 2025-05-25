using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.Commands
{
    public class PayJailFineCommand : IRequest
    {
        public string RoomCode { get; set; } = "";
        public string ConnectionId { get; set; } = "";
        public IHubCallerClients Clients { get; set; } = null!;
    }
}
