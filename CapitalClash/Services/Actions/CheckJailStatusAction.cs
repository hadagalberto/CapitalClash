using CapitalClash.Models;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Services.Actions
{
    public class CheckJailStatusAction : IAction
    {
        public async Task ExecuteAsync(GameContext context)
        {
            if (!context.Player.IsInJail) return;

            if (context.IsDouble)
            {
                context.Player.IsInJail = false;
                context.Player.JailTurns = 0;

                await context.Clients.Group(context.RoomCode).SendAsync("JailEscape", context.Player.Nickname, "dados iguais");
            }
            else
            {
                context.Player.JailTurns++;

                if (context.Player.JailTurns >= 3)
                {
                    context.Player.IsInJail = false;
                    context.Player.JailTurns = 0;
                    await context.Clients.Group(context.RoomCode).SendAsync("JailEscape", context.Player.Nickname, "3 turnos cumpridos");
                }
                else
                {
                    await context.Clients.Group(context.RoomCode).SendAsync("StillInJail", context.Player.Nickname, context.Player.JailTurns);
                    throw new JailHoldException(); // usada para parar o pipeline
                }
            }
        }
    }

    public class JailHoldException : Exception { }

}
