using CapitalClash.Models;

namespace CapitalClash.Services.Actions
{
    public interface IAction
    {
        Task ExecuteAsync(GameContext context);
    }
}
