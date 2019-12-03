using System;
using Domain.Boards;

namespace Domain.Ships
{
    public class Cruiser : Ship
    {
        public Cruiser()
        {
            ShipType = "Cruiser";
            BoardSquareState = BoardSquareState.Cruiser;
            Size = 2;
            Health = 2;
        }

        public Cruiser(int health)
        {
            ShipType = "Cruiser";
            BoardSquareState = BoardSquareState.Cruiser;
            Size = 2;
            Health = health;
        }
    }
}