using CapitalClash.Enums;
using CapitalClash.Models;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Services.Actions
{
    public class EvaluateAvailableActionsAction : IAction
    {
        public async Task ExecuteAsync(GameContext context)
        {
            var actions = new List<string>();
            var space = context.LandedSpace;

            if (space == null)
                return;

            // Pode comprar?
            if (space.Type == BoardSpaceType.Property && space.OwnerId == null)
            {
                if (context.Player.Balance >= (space.Cost ?? 0))
                    actions.Add("buy");
            }

            // Pode melhorar?
            if (space.Type == BoardSpaceType.Property &&
                space.OwnerId == context.Player.Id &&
                space.UpgradeLevel < space.MaxUpgradeLevel)
            {
                int upgradeCost = space.Cost.Value / 2 + (space.UpgradeLevel * 50);
                if (context.Player.Balance >= upgradeCost)
                    actions.Add("upgrade");
            }

            await context.Clients.Caller.SendAsync("AvailableActions", actions);
        }
    }

}
