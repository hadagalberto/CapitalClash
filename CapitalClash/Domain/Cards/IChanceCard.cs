using CapitalClash.Models;
using MediatR;

namespace CapitalClash.Domain.Cards
{
    public interface IChanceCard
    {
        string Name { get; }
        Task ExecuteAsync(GameContext context, IMediator mediator);
    }
}
