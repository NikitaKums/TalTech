using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Boards;
using Domain.Ships;

namespace Domain
{
    public class Player
    {
        public string UserName { get; set; }
        public List<Coordinates> MovesDoneList { get; set; }
        public List<Ship> Ships { get; set; }
        public GamingBoard GamingBoard { get; set; }
        public TrackingBoard TrackingBoard { get; set; }
        public List<Ship> SunkShips { get; set; }
        public int HitPoints { get; set; }
        
        public Player(string name)
        {
            UserName = name;
            MovesDoneList = new List<Coordinates>();
            Ships = new List<Ship>();
            InitializeShips();
            GamingBoard = new GamingBoard();
            TrackingBoard = new TrackingBoard();
            SunkShips = new List<Ship>();
            HitPoints = Ships.Sum(ship => ship.Health);
        }

        public Player(string name, List<Coordinates> movesDoneList, List<Ship> ships, GamingBoard gamingBoard, TrackingBoard trackingBoard, List<Ship> sunkShips, int hitPoints)
        {
            UserName = name;
            MovesDoneList = movesDoneList;
            Ships = ships;
            GamingBoard = gamingBoard;
            SunkShips = sunkShips;
            HitPoints = hitPoints;
            TrackingBoard = trackingBoard;
        }

        public Player()
        {
        }
        

        public bool GenerateRandomBoardWithShips()
        {
            var max = Options.OPTIONS["Board size"];
            
            foreach (var ship in Ships)
            {
                var success = false;
                var safety = 0;
                while (safety < max * 2)
                {
                    success = GamingBoard.PlaceShip(ship, GetDirection());
                    safety++;
                    if (success) break;
                }

                if (success) continue;
                GamingBoard.FillBoard();
                return false;
            }
            
            GamingBoard.RemoveNeighbours(max);
            return true;
        }

        private void InitializeShips()
        {
            for (var i = 0; i < Options.OPTIONS["Carrier amount"]; i++)
            {
                Ships.Add(new Carrier());
            }
            
            for (var i = 0; i < Options.OPTIONS["Battleship amount"]; i++)
            {
                Ships.Add(new BattleShip());
            }
            
            for (var i = 0; i < Options.OPTIONS["Submarine amount"]; i++)
            {
                Ships.Add(new Submarine());
            }
            
            for (var i = 0; i < Options.OPTIONS["Cruiser amount"]; i++)
            {
                Ships.Add(new Cruiser());
            }
            
            for (var i = 0; i < Options.OPTIONS["Patrol amount"]; i++)
            {
                Ships.Add(new Patrol());
            }

        }
        
        private static string GetDirection()
        {
            var random = new Random();
            var number = random.Next(1, 3);
            return number == 1 ? "horizontal" : "vertical";
        }

        public int GetHitpoints()
        {
           return HitPoints;
        }
        public override string ToString()
        {
            return UserName;
        }
    }
}