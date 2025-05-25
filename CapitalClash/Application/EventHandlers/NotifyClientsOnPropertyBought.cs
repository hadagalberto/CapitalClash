using CapitalClash.Application.Events;
using CapitalClash.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.EventHandlers
{
    public class NotifyClientsOnPropertyBought : INotificationHandler<PropertyBought>
    {
        private readonly IHubContext<GameHub> _hubContext;

        public NotifyClientsOnPropertyBought(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(PropertyBought notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
                .Group(notification.RoomCode)
                .SendAsync("PropertyBought", notification.PlayerName, notification.PropertyName);
        }
    }
}
