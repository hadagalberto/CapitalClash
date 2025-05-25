using CapitalClash.Application.Events;
using CapitalClash.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.EventHandlers
{
    public class NotifyClientsOnChance : INotificationHandler<ChanceResolved>
    {
        private readonly IHubContext<GameHub> _hubContext;

        public NotifyClientsOnChance(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(ChanceResolved notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
                .Group(notification.RoomCode)
                .SendAsync("ChanceResult", notification.PlayerName, notification.Message);
        }
    }
}
