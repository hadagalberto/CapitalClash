using CapitalClash.Enums;
using CapitalClash.Models;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Extensions
{
    public static class DebtResolver
    {
        public static async Task<bool> TrySettleDebt(GameContext context)
        {
            var player = context.Player;
            if (player.Balance >= 0) return true;

            var ownedProperties = context.Room.Board.Spaces
                .Where(s => s.Type == BoardSpaceType.Property && s.OwnerId == player.Id)
                .OrderBy(p => p.Cost ?? 0)
                .ToList();

            foreach (var property in ownedProperties)
            {
                int value = (property.Cost ?? 0) / 2; // valor de venda = metade do custo
                player.Balance += value;
                property.OwnerId = null;
                property.UpgradeLevel = 0;

                await context.Clients.Group(context.RoomCode).SendAsync("PropertySold", player.Nickname, property.Name, value);

                if (player.Balance >= 0)
                    return true;
            }

            // Ainda está com saldo negativo → eliminar
            context.Room.Players.Remove(player);
            context.Room.State.PlayerPositions.Remove(player.Id);

            await context.Clients.Group(context.RoomCode).SendAsync("PlayerEliminated", player.Nickname);
            return false;
        }
    }

}
