using CapitalClash.Application.Events;
using CapitalClash.Extensions;
using CapitalClash.Hubs;
using CapitalClash.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Application.EventHandlers
{
    public class ProcessRentPayment : INotificationHandler<RentDue>
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly GameStore _gameStore;

        public ProcessRentPayment(IMediator mediator, IHubContext<GameHub> hubContext, GameStore gameStore)
        {
            _mediator = mediator;
            _hubContext = hubContext;
            _gameStore = gameStore;
        }

        public async Task Handle(RentDue notification, CancellationToken cancellationToken)
        {
            if (!_gameStore.Rooms.TryGetValue(notification.RoomCode, out var room)) return;

            var player = room.Players.FirstOrDefault(p => p.Id == notification.PlayerId);
            var owner = room.Players.FirstOrDefault(p => p.Id == notification.Property.OwnerId);

            if (player == null || owner == null) return;

            var rent = notification.Property.Rent ?? 0;
            player.Balance -= rent;
            owner.Balance += rent;

            await _mediator.Publish(new RentPaid
            {
                RoomCode = notification.RoomCode,
                PlayerName = player.Nickname,
                OwnerName = owner.Nickname,
                PropertyName = notification.Property.Name,
                Amount = rent,
                PlayerBankrupt = player.Balance < 0
            });

            var context = new GameContext
            {
                Room = room,
                Player = player,
                Clients = (IHubCallerClients)_hubContext.Clients,
                RoomCode = notification.RoomCode,
                Mediator = _mediator
            };

            await DebtResolver.TrySettleDebt(context);
        }
    }
}
