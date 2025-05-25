using CapitalClash.Application.Events;
using CapitalClash.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.EventHandlers
{
    public class NotifyClientsOnRentPaid : INotificationHandler<RentPaid>
    {
        private readonly IHubContext<GameHub> _hubContext;

        public NotifyClientsOnRentPaid(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(RentPaid notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
                .Group(notification.RoomCode)
                .SendAsync("RentPaid", notification.PlayerName, notification.OwnerName, notification.PropertyName, notification.Amount);

            if (notification.PlayerBankrupt)
            {
                await _hubContext.Clients
                    .Group(notification.RoomCode)
                    .SendAsync("Bankrupt", notification.PlayerName);
            }
        }
    }
}
