using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CapitalClash.Models
{
    public class GameContext
    {
        public GameRoom Room { get; set; }
        public Player Player { get; set; }
        public IHubCallerClients Clients { get; set; }
        public string RoomCode { get; set; }
        public IMediator Mediator { get; set; }

        public int Dice1 { get; set; }
        public int Dice2 { get; set; }
        public int DiceSum => Dice1 + Dice2;
        public bool IsDouble => Dice1 == Dice2;

        public int Position { get; set; }
        public BoardSpace? LandedSpace => Room.Board.Spaces[Position];
    }

}
