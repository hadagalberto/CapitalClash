using CapitalClash.Application.Commands;
using CapitalClash.Application.Events;
using CapitalClash.Models;
using CapitalClash.Services.Actions;
using MediatR;

namespace CapitalClash.Application.Handlers
{
    public class PayJailFineHandler : IRequestHandler<PayJailFineCommand>
    {
        private readonly IMediator _mediator;
        private readonly GameStore _gameStore;

        public PayJailFineHandler(IMediator mediator, GameStore gameStore)
        {
            _mediator = mediator;
            _gameStore = gameStore;
        }

        public async Task<Unit> Handle(PayJailFineCommand request, CancellationToken cancellationToken)
        {
            if (!_gameStore.Rooms.TryGetValue(request.RoomCode, out var room)) return Unit.Value;

            var player = room.Players.FirstOrDefault(p => p.ConnectionId == request.ConnectionId);
            if (player == null || !player.IsInJail) return Unit.Value;

            int fine = GameConfig.JailFine;
            if (player.Balance < fine) return Unit.Value;

            player.Balance -= fine;
            player.IsInJail = false;
            player.JailTurns = 0;

            await _mediator.Publish(new JailEscape
            {
                RoomCode = request.RoomCode,
                PlayerName = player.Nickname,
                Reason = "pagou fiança"
            });

            return Unit.Value;
        }
    }
}
