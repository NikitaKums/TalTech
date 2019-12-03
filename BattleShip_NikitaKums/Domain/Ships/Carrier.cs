using System;
using Domain.Boards;

namespace Domain.Ships
{
    public class Carrier : Ship
    {
        public Carrier()
        {
            ShipType = "Carrier";
            BoardSquareState = BoardSquareState.Carrier;
            Size = 5;
            Health = 5;
        }
        
        public Carrier(int health)
        {
            ShipType = "Carrier";
            BoardSquareState = BoardSquareState.Carrier;
            Size = 5;
            Health = health;
        }
    }
}