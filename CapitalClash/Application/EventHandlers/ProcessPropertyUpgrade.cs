using CapitalClash.Application.Events;
using CapitalClash.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.EventHandlers
{
    public class ProcessPropertyUpgrade : INotificationHandler<PropertyUpgraded>
    {
        private readonly IHubContext<GameHub> _hubContext;

        public ProcessPropertyUpgrade(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(PropertyUpgraded notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
                .Group(notification.RoomCode)
                .SendAsync("PropertyUpgraded", notification.PlayerName, notification.PropertyName, notification.NewLevel);
        }
    }
}
