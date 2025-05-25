using CapitalClash.Application.Commands;
using CapitalClash.Application.Events;
using CapitalClash.Enums;
using CapitalClash.Models;
using CapitalClash.Services.Actions;
using MediatR;

namespace CapitalClash.Application.Handlers
{
    public class BuyPropertyHandler : IRequestHandler<BuyPropertyCommand>
    {
        private readonly IMediator _mediator;
        private readonly GameStore _gameStore;

        public BuyPropertyHandler(IMediator mediator, GameStore gameStore)
        {
            _mediator = mediator;
            _gameStore = gameStore;
        }

        public async Task<Unit> Handle(BuyPropertyCommand request, CancellationToken cancellationToken)
        {
            if (!_gameStore.Rooms.TryGetValue(request.RoomCode, out var room)) return Unit.Value;

            var player = room.Players.FirstOrDefault(p => p.ConnectionId == request.ConnectionId);
            if (player == null) return Unit.Value;

            var position = room.State.PlayerPositions[player.Id];
            var space = room.Board.Spaces[position];

            if (space.Type != BoardSpaceType.Property || space.OwnerId != null) return Unit.Value;
            if (player.Balance < space.Cost) return Unit.Value;

            player.Balance -= space.Cost.Value;
            space.OwnerId = player.Id;

            await _mediator.Publish(new PropertyBought
            {
                RoomCode = request.RoomCode,
                PlayerId = player.Id,
                PlayerName = player.Nickname,
                PropertyName = space.Name
            });

            return Unit.Value;
        }
    }
}
