using CapitalClash.Application.Commands;
using CapitalClash.Application.Events;
using CapitalClash.Enums;
using CapitalClash.Models;
using CapitalClash.Services.Actions;
using MediatR;

namespace CapitalClash.Application.Handlers
{
    public class UpgradePropertyHandler : IRequestHandler<UpgradePropertyCommand>
    {
        private readonly IMediator _mediator;
        private readonly GameStore _gameStore;

        public UpgradePropertyHandler(IMediator mediator, GameStore gameStore)
        {
            _mediator = mediator;
            _gameStore = gameStore;
        }

        public async Task<Unit> Handle(UpgradePropertyCommand request, CancellationToken cancellationToken)
        {
            if (!_gameStore.Rooms.TryGetValue(request.RoomCode, out var room)) return Unit.Value;

            var player = room.Players.FirstOrDefault(p => p.ConnectionId == request.ConnectionId);
            if (player == null) return Unit.Value;

            var position = room.State.PlayerPositions[player.Id];
            var space = room.Board.Spaces[position];

            if (space.Type != BoardSpaceType.Property || space.OwnerId != player.Id) return Unit.Value;
            if (space.UpgradeLevel >= space.MaxUpgradeLevel) return Unit.Value;

            int upgradeCost = space.Cost.Value / 2 + (space.UpgradeLevel * 50);
            if (player.Balance < upgradeCost) return Unit.Value;

            player.Balance -= upgradeCost;
            space.UpgradeLevel++;
            space.Rent += 10 * space.UpgradeLevel;

            await _mediator.Publish(new PropertyUpgraded
            {
                RoomCode = request.RoomCode,
                PlayerName = player.Nickname,
                PropertyName = space.Name,
                NewLevel = space.UpgradeLevel
            });

            return Unit.Value;
        }
    }
}
