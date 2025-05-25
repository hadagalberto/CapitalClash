using CapitalClash.Application.Events;
using CapitalClash.Enums;
using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public class GoToJailCard : IChanceCard
    {
        public string Name => "Foi direto para a prisão!";

        public async Task ExecuteAsync(GameContext context, IMediator mediator)
        {
            var jailIndex = context.Room.Board.Spaces.FirstOrDefault(s => s.Type == BoardSpaceType.Jail)?.Index ?? 0;
            context.Room.State.PlayerPositions[context.Player.Id] = jailIndex;
            context.Player.IsInJail = true;
            context.Player.JailTurns = 0;

            await mediator.Publish(new ChanceResolved
            {
                RoomCode = context.RoomCode,
                PlayerName = context.Player.Nickname,
                Message = Name
            });
        }
    }
}
