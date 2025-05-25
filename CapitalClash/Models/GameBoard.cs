using CapitalClash.Enums;

namespace CapitalClash.Models
{
    public class GameBoard
    {

        public List<BoardSpace> Spaces { get; set; } = new();

        public static GameBoard CreateDefaultBoard()
        {
            return new GameBoard
            {
                Spaces = new List<BoardSpace>
                {
                    new BoardSpace { Index = 0, Name = "Início", Type = BoardSpaceType.Start },

                    new BoardSpace { Index = 1, Name = "Brasília", Type = BoardSpaceType.Property, Cost = 100, Rent = 10 },
                    new BoardSpace { Index = 2, Name = "Chance", Type = BoardSpaceType.Chance },
                    new BoardSpace { Index = 3, Name = "Salvador", Type = BoardSpaceType.Property, Cost = 120, Rent = 12 },
                    new BoardSpace { Index = 4, Name = "Imposto", Type = BoardSpaceType.Tax, Rent = 50 },

                    new BoardSpace { Index = 5, Name = "Estacionamento Livre", Type = BoardSpaceType.FreeParking },
                    new BoardSpace { Index = 6, Name = "São Paulo", Type = BoardSpaceType.Property, Cost = 140, Rent = 14 },
                    new BoardSpace { Index = 7, Name = "Chance", Type = BoardSpaceType.Chance },
                    new BoardSpace { Index = 8, Name = "Belo Horizonte", Type = BoardSpaceType.Property, Cost = 160, Rent = 16 },
                    new BoardSpace { Index = 9, Name = "Ir para Prisão", Type = BoardSpaceType.GoToJail },

                    new BoardSpace { Index = 10, Name = "Rio de Janeiro", Type = BoardSpaceType.Property, Cost = 180, Rent = 18 },
                    new BoardSpace { Index = 11, Name = "Chance", Type = BoardSpaceType.Chance },
                    new BoardSpace { Index = 12, Name = "Manaus", Type = BoardSpaceType.Property, Cost = 200, Rent = 20 },
                    new BoardSpace { Index = 13, Name = "Imposto de Renda", Type = BoardSpaceType.Tax, Rent = 75 },
                    new BoardSpace { Index = 14, Name = "Porto Alegre", Type = BoardSpaceType.Property, Cost = 220, Rent = 22 },

                    new BoardSpace { Index = 15, Name = "Estacionamento Livre", Type = BoardSpaceType.FreeParking },
                    new BoardSpace { Index = 16, Name = "Curitiba", Type = BoardSpaceType.Property, Cost = 240, Rent = 24 },
                    new BoardSpace { Index = 17, Name = "Chance", Type = BoardSpaceType.Chance },
                    new BoardSpace { Index = 18, Name = "Recife", Type = BoardSpaceType.Property, Cost = 260, Rent = 26 },
                    new BoardSpace { Index = 19, Name = "Fortaleza", Type = BoardSpaceType.Property, Cost = 280, Rent = 28 },

                    new BoardSpace { Index = 20, Name = "Goiania", Type = BoardSpaceType.Property, Cost = 300, Rent = 30 },
                    new BoardSpace { Index = 21, Name = "Belém", Type = BoardSpaceType.Property, Cost = 320, Rent = 32 },
                    new BoardSpace { Index = 22, Name = "Chance Final", Type = BoardSpaceType.Chance },
                    new BoardSpace { Index = 23, Name = "Natal", Type = BoardSpaceType.Property, Cost = 340, Rent = 34 },
                    new BoardSpace { Index = 24, Name = "Último Imposto", Type = BoardSpaceType.Tax, Rent = 100 }
                }
            };
        }
    }
}
