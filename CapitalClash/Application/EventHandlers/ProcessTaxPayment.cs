using CapitalClash.Application.Events;
using CapitalClash.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.EventHandlers
{
    public class ProcessTaxPayment : INotificationHandler<TaxCharged>
    {
        private readonly IHubContext<GameHub> _hubContext;

        public ProcessTaxPayment(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(TaxCharged notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
                .Group(notification.RoomCode)
                .SendAsync("TaxCharged", notification.PlayerName, notification.Amount);
        }
    }
}
