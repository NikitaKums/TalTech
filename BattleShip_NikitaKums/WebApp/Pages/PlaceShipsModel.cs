using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Boards;
using Domain.Ships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaveSystem;

namespace WebApp.Pages
{
    public class PlaceShipsModel : PageModel
    {  
        [BindProperty]
        public int Index { get; set; }
        public bool Error { get; set; }
        
        public Player Player { get; set; }
        public Computer Computer { get; set; }
        
        public List<string> ShipsToPlace { get; set; } = new List<string>();
        
        public IActionResult OnGet(int index)
        {
            Index = index;
            var save = GameSaveRead.LoadSaveFromDb(Index);
            Player = save[0];
            Computer = (Computer)save[1];
            GetShipsThatCanBePlaced();
            return Page();
        }

        public IActionResult OnPost(string[] coordinates, string submit)
        {
            var save = GameSaveRead.LoadSaveFromDb(Index);
            Player = save[0];
            Computer = (Computer)save[1];
            
            if (submit.Equals("Continue"))
            {
                Player.GamingBoard.RemoveNeighbours(Player.GamingBoard.BoardSize);
                GameSaveRead.OverwriteSave(Index, Player, Computer);
                return RedirectToPage("PlayGame", "FirstEntry", new {index = Index});
            }
            
            if (coordinates.Length > 5 || coordinates.Length <= 1 && string.IsNullOrWhiteSpace(coordinates[0]))
            {
                GetShipsThatCanBePlaced();
                ModelState.AddModelError("Error", "Invalid coordinates");
                return Page();
            }
            if (!PlaceShipsOnBoard(coordinates))
            {
                ModelState.AddModelError("Error", "All ships with that size have already been placed");
            }
            GetShipsThatCanBePlaced();
            GameSaveRead.OverwriteSave(Index, Player, Computer);
            return Page();
        }

        private bool PlaceShipsOnBoard(string[] coordinates)
        {
            int amount;
            var coordinatesFromInput = GetCoordinates(coordinates);
            Ship ship;
            if (coordinates.Length == 5)
            {
                amount = CountShips(Options.OPTIONS["Carrier amount"], BoardSquareState.Carrier);
                if (amount == 0) return false;
                ship = Player.Ships.Find(s => s.BoardSquareState == BoardSquareState.Carrier && s.ShipCoordinates.Count == 0);
                
                if (Player.GamingBoard.SetShipUsingCoordinates(coordinatesFromInput, ship))
                    ship.ShipCoordinates = coordinatesFromInput;
            }
            else if (coordinates.Length == 4)
            {
                amount = CountShips(Options.OPTIONS["Battleship amount"], BoardSquareState.BattleShip);
                if (amount == 0) return false;
                ship = Player.Ships.Find(s => s.BoardSquareState == BoardSquareState.BattleShip && s.ShipCoordinates.Count == 0);
                
                if (Player.GamingBoard.SetShipUsingCoordinates(coordinatesFromInput, ship))
                    ship.ShipCoordinates = coordinatesFromInput;
            }
            else if (coordinates.Length == 3)
            {
                amount = CountShips(Options.OPTIONS["Submarine amount"], BoardSquareState.Submarine);
                if (amount == 0) return false;
                ship = Player.Ships.Find(s => s.BoardSquareState == BoardSquareState.Submarine && s.ShipCoordinates.Count == 0);
                
                if (Player.GamingBoard.SetShipUsingCoordinates(coordinatesFromInput, ship))
                    ship.ShipCoordinates = coordinatesFromInput;
                
            }
            else if (coordinates.Length == 2)
            {
                amount = CountShips(Options.OPTIONS["Cruiser amount"], BoardSquareState.Cruiser);
                if (amount == 0) return false;
                ship = Player.Ships.Find(s => s.BoardSquareState == BoardSquareState.Cruiser && s.ShipCoordinates.Count == 0);
                
                if (Player.GamingBoard.SetShipUsingCoordinates(coordinatesFromInput, ship))
                    ship.ShipCoordinates = coordinatesFromInput;
            }
            else if (coordinates.Length == 1)
            {
                amount = CountShips(Options.OPTIONS["Patrol amount"], BoardSquareState.Patrol);
                if (amount == 0) return false;
                ship = Player.Ships.Find(s => s.BoardSquareState == BoardSquareState.Patrol && s.ShipCoordinates.Count == 0);
                
                if (Player.GamingBoard.SetShipUsingCoordinates(coordinatesFromInput, ship))
                    ship.ShipCoordinates = coordinatesFromInput;
            }

            return true;

        }

        private void GetShipsThatCanBePlaced()
        {
            foreach (var ship in Player.Ships)
            {
                if (ship.ShipCoordinates.Count == 0) 
                    ShipsToPlace.Add(ship.ShipType + " | size: " + ship.Size);
            }
            if (ShipsToPlace.Count == 0) ShipsToPlace.Add(" ");
        }
        
        private int CountShips(int optionsAmount, BoardSquareState boardSquareState)
        {
            return optionsAmount - Player.Ships.Count(s => s.BoardSquareState == boardSquareState && s.ShipCoordinates.Count != 0);
        }

        private List<Coordinates> GetCoordinates(string[] coordinates)
        {
            return coordinates.Select(coordinate => coordinate.Split("!")).Select(temp => new Coordinates(int.Parse(temp[0]), Coordinates.IntToYCoordinate(int.Parse(temp[1])))).ToList();
        }
    }
}