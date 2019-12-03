using System;
using Domain.Boards;

namespace Domain.Ships
{
    public class BattleShip : Ship
    {
        public BattleShip()
        {
            ShipType = "BattleShip";
            BoardSquareState = BoardSquareState.BattleShip;
            Size = 4;
            Health = 4;
        }

        public BattleShip(int health)
        {
            ShipType = "BattleShip";
            BoardSquareState = BoardSquareState.BattleShip;
            Size = 4;
            Health = health;
        }
    }
}