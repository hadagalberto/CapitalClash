using CapitalClash.Extensions;
using CapitalClash.Models;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Services.Actions
{
    public class RollDiceAction : IAction
    {
        public async Task ExecuteAsync(GameContext context)
        {
            var rnd = new Random();
            context.Dice1 = rnd.Next(1, 7);
            context.Dice2 = rnd.Next(1, 7);

            context.Room.State.DiceRoll = context.DiceSum;

            var currentPosition = context.Room.State.PlayerPositions[context.Player.Id];
            var newPosition = (currentPosition + context.DiceSum) % context.Room.Board.Spaces.Count;
            context.Player.Position = newPosition;

            bool passedStart = newPosition < currentPosition;
            bool landedOnStart = newPosition == 0;

            if (passedStart || landedOnStart)
            {
                int bonus = landedOnStart ? (int)(GameConfig.StartBonusAmount * 1.5) : GameConfig.StartBonusAmount;

                int received = context.Player.ApplyEarnings(bonus);

                await context.Clients
                    .Group(context.RoomCode)
                    .SendAsync("BonusStart", context.Player.Nickname, received);
            }

            context.Room.State.PlayerPositions[context.Player.Id] = newPosition;
            context.Position = newPosition;

            await context.Clients.Group(context.RoomCode).SendAsync("DiceRolled", context.Player.Nickname, context.Dice1, context.Dice2);
            await context.Clients.Group(context.RoomCode).SendAsync("Landed", context.Player.Nickname, context.LandedSpace?.Name);
        }
    }
}
