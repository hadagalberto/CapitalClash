using CapitalClash.Application.Events;
using CapitalClash.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.EventHandlers
{
    public class NotifyClientsOnJailEscape : INotificationHandler<JailEscape>
    {
        private readonly IHubContext<GameHub> _hubContext;

        public NotifyClientsOnJailEscape(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(JailEscape notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
                .Group(notification.RoomCode)
                .SendAsync("JailEscape", notification.PlayerName, notification.Reason);
        }
    }
}
