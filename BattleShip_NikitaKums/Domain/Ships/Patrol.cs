using System;
using Domain.Boards;

namespace Domain.Ships
{
    public class Patrol : Ship
    {
        public Patrol()
        {
            ShipType = "Patrol";
            BoardSquareState = BoardSquareState.Patrol;
            Size = 1;
            Health = 1;
        }
        public Patrol(int health)
        {
            ShipType = "Patrol";
            BoardSquareState = BoardSquareState.Patrol;
            Size = 1;
            Health = health;
        }
    }
}