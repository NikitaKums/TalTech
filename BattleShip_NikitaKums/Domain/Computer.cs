using System;
using System.Collections.Generic;
using Domain.Boards;
using Domain.Ships;

namespace Domain
{
    public class Computer : Player
    {
        public List<Coordinates> AllPossibleCoordinates { get; set; }

        public Computer(string name = "Sally") : base(name)
        {
            AllPossibleCoordinates = GetAllPossibleCoordinates();
        }

        public Computer(string name, List<Coordinates> movesDoneList, List<Ship> ships, GamingBoard gamingBoard, TrackingBoard trackingBoard, List<Ship> sunkShips, int hitPoints,
            List<Coordinates> allPossibleCoordinates) : base(name, movesDoneList, ships, gamingBoard, trackingBoard, sunkShips, hitPoints)
        {
            UserName = name;
            MovesDoneList = movesDoneList;
            Ships = ships;
            GamingBoard = gamingBoard;
            TrackingBoard = trackingBoard;
            SunkShips = sunkShips;
            HitPoints = hitPoints;
            AllPossibleCoordinates = allPossibleCoordinates;
        }

        public Coordinates MakeMove()
        {
            var random = new Random();
            var randomInt = random.Next(0, AllPossibleCoordinates.Count);
            var coordinate = AllPossibleCoordinates[randomInt];
            AllPossibleCoordinates.RemoveAt(randomInt);
            return coordinate;
        }

        private List<Coordinates> GetAllPossibleCoordinates()
        {
            var coordinatesList = new List<Coordinates>();
            var max = Options.OPTIONS["Board size"];
            for (var i = 0; i < max; i++)
            {
                for (var j = 0; j < max; j++)
                {
                    coordinatesList.Add(new Coordinates(i, Coordinates.IntToYCoordinate(j)));
                }
            }

            return coordinatesList;
        }
    }
}