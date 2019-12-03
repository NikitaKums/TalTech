using System;
using Domain.Boards;

namespace Domain.Ships
{
    public class Submarine : Ship
    {
        public Submarine()
        {
            ShipType = "Submarine";
            BoardSquareState = BoardSquareState.Submarine;
            Size = 3;
            Health = 3;
        }
        public Submarine(int health)
        {
            ShipType = "Submarine";
            BoardSquareState = BoardSquareState.Submarine;
            Size = 3;
            Health = health;
        }
    }
}