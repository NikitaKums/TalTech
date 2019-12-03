using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Ships;

namespace Domain.Boards
{
    public class Boards
    {
        protected BoardSquareState[,] Board { get; set; }
        public string BoardName { get; set; }
        public int BoardSize = Options.OPTIONS["Board size"];
        
        // REMINDER: Can be used to reset board
        public void FillBoard()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    Board[i, j] = BoardSquareState.Water;
                }
            }
        }
        
        public BoardSquareState this[int x, int y]
        {
            get => Board[x, y];
            set => Board[x, y] = value;
        }

        public BoardSquareState BoardSquareStateFromString(string boardSquare)
        {
            switch (boardSquare)
            {
                 case "Water":
                     return BoardSquareState.Water;
                 case "Carrier":
                     return BoardSquareState.Carrier;
                 case "BattleShip":
                     return BoardSquareState.BattleShip;
                 case "Submarine":
                     return BoardSquareState.Submarine;
                 case "Cruiser":
                     return BoardSquareState.Cruiser;
                 case "Patrol":
                     return BoardSquareState.Patrol;
                 case "Hit":
                     return BoardSquareState.Hit;
                 case "Miss":
                     return BoardSquareState.Miss;
                 case "Dead":
                     return BoardSquareState.Dead;
                 case "Neighbour":
                     return BoardSquareState.Neighbour;
                 default:
                     throw new ArgumentOutOfRangeException(nameof(boardSquare), boardSquare, null);
            }
        }


        public bool PlaceShip(Ship ship, string direction)
        {
            var coordinatesList = new List<Coordinates>();
            var max = Options.OPTIONS["Board size"];
            var shipSize = ship.Size;
            
            var randomGenerator = new Random();
            var randomX = randomGenerator.Next(0, max/2); // random x cord
            var randomY = randomGenerator.Next(0, max/2); // random y cord
            
            var horizontal = direction.Equals("horizontal");
            
            for (var x = randomX; x < max; x++)
            {
                var spotsInARow = 0;
                coordinatesList.Clear();
                
                for (var y = randomY; y < max; y++)
                {
                    if (horizontal)
                    {
                        if (Board[x, y] == BoardSquareState.Water)
                        {
                            spotsInARow++;
                            coordinatesList.Add(new Coordinates(x, Coordinates.IntToYCoordinate(y)));
                        }
                        else
                        {
                            spotsInARow = 0;
                            coordinatesList.Clear();
                        }
                    } 
                    else
                    {
                        if (Board[y, x] == BoardSquareState.Water)
                        {
                            spotsInARow++;
                            coordinatesList.Add(new Coordinates(y, Coordinates.IntToYCoordinate(x)));
                        }
                        else
                        {
                            spotsInARow = 0;
                            coordinatesList.Clear();
                        }
                    }
                    if (spotsInARow != shipSize) continue;
                    ParseNeighboursAndSetShip(ship, coordinatesList);
                    return true;
                }
            }

            return false;
        }
        
        private void ParseNeighboursAndSetShip(Ship ship, List<Coordinates> coordinates)
        {
            var boardSquareState = ship.BoardSquareState;
            if (Options.OPTIONS["Can touch"] == 0) // cannot touch
            {
                foreach (var coordinate in coordinates)
                {
                    var xCord = coordinate.GetX();
                    var yCord = coordinate.GetY();
                    for (var i = -1; i < 2; i++)
                    {
                        TryPlacingNeighbour(xCord + i, yCord - 1);
                        TryPlacingNeighbour(xCord + i, yCord);
                        TryPlacingNeighbour(xCord + i, yCord + 1);
                    }
                }
            }
            foreach (var coordinate in coordinates)
            {
                Board[coordinate.GetX(), coordinate.GetY()] = boardSquareState;
                ship.ShipCoordinates.Add(coordinate);
            }
        }
        
        public bool SetShipUsingCoordinates(List<Coordinates> coordinates, Ship ship)
        {
            var previousXCord = coordinates[0].GetX();
            var previousYCord = coordinates[0].GetY();
            foreach (var coordinate in coordinates)
            {
                var xCord = coordinate.GetX();
                var yCord = coordinate.GetY();
                if (Board[xCord, yCord] != BoardSquareState.Water)
                {
                    return false;
                }

                if (Math.Abs(xCord - previousXCord) > 1 || Math.Abs(yCord - previousYCord) > 1)
                {
                    return false;
                }

                if (Math.Abs(xCord - previousXCord) == 1 && Math.Abs(yCord - previousYCord) == 1)
                {
                    return false;
                }
                previousXCord = xCord;
                previousYCord = yCord;
            }
            ParseNeighboursAndSetShip(ship, coordinates);
            return true;
        }

        private void TryPlacingNeighbour(int x, int y)
        {
            try
            {
                Board[x, y] = BoardSquareState.Neighbour;
            }
            catch (IndexOutOfRangeException)
            {}
        }
        
        public void RemoveNeighbours(int max)
        {
            for (var i = 0; i < max; i++)
            {
                for (var j = 0; j < max; j++)
                {
                    if (Board[i, j] == BoardSquareState.Neighbour)
                    {
                        Board[i, j] = BoardSquareState.Water;
                    }
                }
            }
        }

        public static bool ParseShot(Coordinates coordinates, Player attacker, Player defender, bool isReplay = false) // return false if not a hit
        {
            var xCord = coordinates.GetX();
            var yCord = coordinates.GetY();
            
            if (defender.GamingBoard[xCord, yCord] == BoardSquareState.Hit ||
                defender.GamingBoard[xCord, yCord] == BoardSquareState.Miss)
            {
                return false;
            }
            attacker.MovesDoneList.Add(coordinates);
            if (defender.GamingBoard[xCord, yCord] == BoardSquareState.Water)
            {
                defender.GamingBoard[xCord, yCord] = BoardSquareState.Miss;
                attacker.TrackingBoard[xCord, yCord] = BoardSquareState.Miss;
                return false;
            }
            attacker.TrackingBoard[xCord, yCord] = BoardSquareState.Hit;
            defender.GamingBoard[xCord, yCord] = BoardSquareState.Hit;
            
            if (isReplay) return true;
            
            var shipThatWasHit = Ship.GetShipAtCoordinates(defender, xCord, yCord);
            shipThatWasHit.Health--;
            if (shipThatWasHit.Health == 0)
            {
                defender.SunkShips.Add(shipThatWasHit);
            }
            defender.HitPoints--;
            
            return true;
        }
        
        public string CurrentBoardName()
        {
            return BoardName;
        }
    }
}