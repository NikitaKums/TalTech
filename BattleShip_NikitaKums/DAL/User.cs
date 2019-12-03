using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Boards;
using Newtonsoft.Json;

namespace DAL
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        
        public string MovesDone { get; set; }
        public int HitPoints { get; set; }
        public string AllCoordinates { get; set; }
        public List<Board> Boards { get; set; } = new List<Board>();
        public List<Ship> Ships { get; set; } = new List<Ship>();

        
        public Player GetDomainPlayer(List<Domain.Ships.Ship> ships, GamingBoard gamingBoard, TrackingBoard trackingBoard ,List<Domain.Ships.Ship> sunkShips)
        {
            return new Player(Name, GetMovesDoneList(), ships, gamingBoard, trackingBoard,sunkShips, ships.Sum(c => c.Health));
        } // end GetDomainPlayer

        
        public Computer GetDomainComputer(List<Domain.Ships.Ship> ships, GamingBoard gamingBoard, TrackingBoard trackingBoard ,List<Domain.Ships.Ship> sunkShips)
        {
            var hitPoints = ships.Sum(c => c.Health);
            var allPossibleCoordinates = new List<Coordinates>();
            var allCoordinatesDeserialized = JsonConvert.DeserializeObject<List<int>>(AllCoordinates);
            var i = 0;
            while (i < allCoordinatesDeserialized.Count)
            {
                allPossibleCoordinates.Add(new Coordinates(allCoordinatesDeserialized[i], Coordinates.IntToYCoordinate(allCoordinatesDeserialized[i+1])));
                i += 2;
            }
            return new Computer(Name, GetMovesDoneList(), ships, gamingBoard, trackingBoard ,sunkShips, hitPoints, allPossibleCoordinates);
        } // end GetDomainComputer

        
        private List<Coordinates> GetMovesDoneList()
        {
            var result = new List<Coordinates>();            
            var allMovesDeserialized = JsonConvert.DeserializeObject<List<int>>(MovesDone);

            var i = 0;
            while (i < allMovesDeserialized.Count)
            {
                result.Add(new Coordinates(allMovesDeserialized[i], Coordinates.IntToYCoordinate(allMovesDeserialized[i+1])));
                i += 2;
            }
            return result;
        } // end GetMovesDoneList

        /*public List<Domain.Ships.Ship> GetUserShips()
        {
            var ships = new List<Domain.Ships.Ship>();
            foreach (var ship in Ships)
            {
                ships.Add(ship.GetDomainShip());
            }

            return ships;
        }*/
    }
}