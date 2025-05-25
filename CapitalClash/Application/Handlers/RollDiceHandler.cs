using CapitalClash.Application.Commands;
using CapitalClash.Models;
using CapitalClash.Services;
using MediatR;

namespace CapitalClash.Application.Handlers
{
    public class RollDiceHandler : IRequestHandler<RollDiceCommand>
    {
        private readonly GameService _gameService;
        private readonly GameStore _gameStore;

        public RollDiceHandler(GameService gameService, GameStore gameStore)
        {
            _gameService = gameService;
            _gameStore = gameStore;
        }

        public async Task<Unit> Handle(RollDiceCommand request, CancellationToken cancellationToken)
        {
            if (!_gameStore.Rooms.TryGetValue(request.RoomCode, out var room)) return Unit.Value;

            var player = room.Players.FirstOrDefault(p => p.ConnectionId == request.ConnectionId);
            if (player == null || room.CurrentTurnPlayerId != player.Id) return Unit.Value;

            await _gameService.ExecuteTurn(room, player, request.Clients, request.RoomCode);

            return Unit.Value;
        }
    }
}
