using System;
using System.Collections.Generic;
using Domain.Boards;

namespace Domain.Ships
{
    public class Ship
    {
        public string ShipType { get; set; }
        public int Size { get; set; }
        public int Health { get; set; }
        public BoardSquareState BoardSquareState { get; set; }
        public List<Coordinates> ShipCoordinates { get; set; } = new List<Coordinates>();


        public static Ship GetShipAtCoordinates(Player player, int xCord, int yCord)
        {
            foreach (var ship in player.Ships)
            {
                foreach (var coordinate in ship.ShipCoordinates)
                {
                    if (coordinate.GetX() == xCord && coordinate.GetY() == yCord) return ship;
                }
            }
            //should never throw
            throw new ArgumentException("No such ship");
        }

        public Ship GetShipByName(string name, int health)
        {
            switch (name)
            {
                case "Carrier":
                    return new Carrier(health);
                case "BattleShip":
                    return new BattleShip(health);
                case "Submarine":
                    return new Submarine(health);
                case "Cruiser":
                    return new Cruiser(health);
                case "Patrol":
                    return new Patrol(health);
                default:
                    throw new ArgumentOutOfRangeException(nameof(name), name, null);
            }
        }
        
        public override string ToString()
        {
            return ShipType;
        }
    }
}