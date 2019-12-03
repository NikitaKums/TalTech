using System.Collections.Generic;
using Domain.Boards;

namespace DAL
{
    public class Ship
    {
        public int ShipId { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        
        public List<ShipLocation> ShipsLocation { get; set; } = new List<ShipLocation>();
        
        public int UserId { get; set; }
        public User User { get; set; }

        public Domain.Ships.Ship GetDomainShip()
        {
            var temp = new Domain.Ships.Ship();
            var ship = temp.GetShipByName(Name, Health);
            foreach (var location in ShipsLocation)
            {
                ship.ShipCoordinates.Add(new Coordinates(location.X, Coordinates.IntToYCoordinate(location.Y)));
            }

            return ship;
        }
    }
}