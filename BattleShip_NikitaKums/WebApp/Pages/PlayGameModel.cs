using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoardUI;
using Domain;
using Domain.Boards;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class PlayGameModel : PageModel
    {   
        [BindProperty]
        public int Index { get; set; }
        [BindProperty]
        [Required]
        public string XCord { get; set; }
        [BindProperty]
        [Required]
        public string YCord { get; set; }
            
        public string Message { get; set; }
        public bool Winner { get; set; }
                
        public List<string> LettersForWebBoard { get; set; }
        
        public Player Player { get; set; }
        public Computer Computer { get; set; }
        
        public IActionResult OnGetFirstEntry(int index)
        {
            Index = index;
            XCord = null; // for when to print results
            Player = SaveSystem.GameSaveRead.LoadSaveFromDb(Index)[0];
            LettersForWebBoard = GetLettersForWebBoard();
            return Page();
        }

        public IActionResult OnPost()
        {
            var save = SaveSystem.GameSaveRead.LoadSaveFromDb(Index);
            Player = save[0];
            Computer = (Computer)save[1];
            LettersForWebBoard = GetLettersForWebBoard();
            if (!Coordinates.ValidateCoordinates(XCord, YCord))
            {
                ModelState.AddModelError("YCord", "Invalid coordinates");
                return Page();
            }
                        
            var playerShotCoordinates = new Coordinates(Convert.ToInt32(XCord), YCord);
            
            Console.WriteLine("Player attacking");
            //ParseShot(Coordinates, attacker, defender)
            if (Boards.ParseShot(playerShotCoordinates, Player, Computer))
            {
                
                if (Computer.HitPoints <= 0)
                {
                    Message = Player.UserName;
                    Winner = true;
                    SunkenShipsParser();
                    SaveSystem.GameSaveRead.OverwriteSave(Index, Player, Computer);
                    return Page();
                }
            }

            Console.WriteLine("Computer attacking");

            var computerShotCoordinates = Computer.MakeMove(); 
            Console.WriteLine(computerShotCoordinates.XCord + " " + computerShotCoordinates.YCord);
            if (Boards.ParseShot(computerShotCoordinates, Computer, Player))
            {
                if (Player.HitPoints <= 0)
                {
                    Message = Computer.UserName;
                    Winner = true;
                    SunkenShipsParser();
                    SaveSystem.GameSaveRead.OverwriteSave(Index, Player, Computer);
                    return Page();
                }
            }
            SunkenShipsParser();
            SaveSystem.GameSaveRead.OverwriteSave(Index, Player, Computer);
            return Page();
        }

        public string GetSymbolsForWebBoard(BoardSquareState boardSquareState)
        {
            switch (boardSquareState)
            {
                case BoardSquareState.Water: return " ";
                case BoardSquareState.Carrier: return "🚢";
                case BoardSquareState.BattleShip: return "🚢";
                case BoardSquareState.Submarine: return "🚢";
                case BoardSquareState.Cruiser: return "🚢";
                case BoardSquareState.Patrol: return "🚢";
                case BoardSquareState.Hit: return "🎯";
                case BoardSquareState.Miss: return "❌";
                case BoardSquareState.Neighbour: return " ";
                case BoardSquareState.Dead: return "☠️";
                default:
                    throw new ArgumentOutOfRangeException(nameof(boardSquareState), boardSquareState, null);
            }
        }

        public List<string> GetLettersForWebBoard()
        {
            return new BoardPrintingUI().GetLettersForBoard(Options.OPTIONS["Board size"])
                .Split(new []{" "}, StringSplitOptions.RemoveEmptyEntries).ToList();

        }

        private void SunkenShipsParser()
        {
            if (Computer.SunkShips.Count <= 0 && Player.SunkShips.Count <= 0) return;
            foreach (var sunkShip in Computer.SunkShips)
            {
                foreach (var coordinate in sunkShip.ShipCoordinates)
                {
                    Player.TrackingBoard[coordinate.GetX(), coordinate.GetY()] = BoardSquareState.Dead;
                }
            }
            
            foreach (var sunkShip in Player.SunkShips)
            {
                foreach (var coordinate in sunkShip.ShipCoordinates)
                {
                    Player.GamingBoard[coordinate.GetX(), coordinate.GetY()] = BoardSquareState.Dead;
                }
            }
        }// end SunkenShipsParser
        
    }
}