using CapitalClash.Application.Events;
using CapitalClash.Enums;
using CapitalClash.Models;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Services.Actions
{
    public class LandOnPropertyAction : IAction
    {
        public async Task ExecuteAsync(GameContext context)
        {
            var space = context.LandedSpace;

            if (space?.Type == BoardSpaceType.Property &&
                space.OwnerId != null &&
                space.OwnerId != context.Player.Id)
            {
                await context.Mediator.Publish(new RentDue
                {
                    RoomCode = context.RoomCode,
                    PlayerId = context.Player.Id,
                    Property = space
                });
            }

            if (space?.Type == BoardSpaceType.Chance)
            {
                await context.Mediator.Publish(new ChanceTriggered
                {
                    RoomCode = context.RoomCode,
                    PlayerId = context.Player.Id
                });
            }

            if (space.Type == BoardSpaceType.Tax)
            {
                var properties = context.Room.Board.Spaces
                    .Where(s => s.Type == BoardSpaceType.Property && s.OwnerId == context.Player.Id)
                    .ToList();

                var totalValue = properties.Sum(p => p.Cost ?? 0);
                var amount = (int)(totalValue * 0.10); // 10% do valor total das propriedades

                context.Player.Balance = Math.Max(0, context.Player.Balance - amount);

                await context.Mediator.Publish(new TaxCharged
                {
                    RoomCode = context.RoomCode,
                    PlayerName = context.Player.Nickname,
                    Amount = amount
                });
            }

            if (space.Type == BoardSpaceType.FreeParking || space.Type == BoardSpaceType.Jail)
            {
                await context.Clients
                    .Group(context.RoomCode)
                    .SendAsync("LandedNeutral", context.Player.Nickname, space.Name);
            }

        }
    }
}
