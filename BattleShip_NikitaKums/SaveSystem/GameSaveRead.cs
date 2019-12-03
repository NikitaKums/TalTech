using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DAL;
using Domain;
using Domain.Boards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Newtonsoft.Json;

namespace SaveSystem
{
    
    public static class GameSaveRead
    {
        public static int Save(Player player, Computer computer)
        {
            var ctx = new AppDbContext();
            var save = new Save
            {
                Date = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                Options = JsonConvert.SerializeObject(Options.OPTIONS),
                PlayerName = player.UserName,
                Winner = GetWinner(player, computer),
                User1 = SavePlayer(player),
                User2 = SavePlayer(null, computer)
            };
            ctx.Saves.Add(save);
            ctx.SaveChanges();
            return save.SaveId;
        }

        private static string GetWinner(Player player, Computer computer)
        {
            if (player.HitPoints == 0)
            {
                return "Computer";
            }
            return computer.HitPoints == 0 ? "Player" : "None";
        }

        private static User SavePlayer(Player player = null, Computer computer = null)
        {
            player = player ?? computer;
            var savePlayer = new User {Name = player.UserName, HitPoints = player.HitPoints};

            //computer all possible coordinates
            if (computer != null)
            {
                var allPossibleCoordinatesForSave = new List<int>();
                foreach (var coordinate in computer.AllPossibleCoordinates)
                {
                    allPossibleCoordinatesForSave.Add(coordinate.XCord);
                    allPossibleCoordinatesForSave.Add(coordinate.GetY());
                }
                savePlayer.AllCoordinates = JsonConvert.SerializeObject(allPossibleCoordinatesForSave);
            }
            
            // moves done for player
            var playerCoordinatesList = new List<int>();
            foreach (var coordinate in player.MovesDoneList)
            {
                playerCoordinatesList.Add(coordinate.XCord);
                playerCoordinatesList.Add(coordinate.GetY());
            }
            savePlayer.MovesDone = JsonConvert.SerializeObject(playerCoordinatesList);
            
            // player GamingBoard/TrackingBoard
            var gb = new Board {BoardName = "GamingBoard", Size = player.GamingBoard.BoardSize};
            var tb = new Board {BoardName = "TrackingBoard", Size = player.TrackingBoard.BoardSize};
            for (var i = 0; i < gb.Size; i++)
            {
                for (var j = 0; j < gb.Size; j++)
                {
                    gb.BoardSquares.Add(new BoardSquare(){Value = player.GamingBoard[i, j].ToString(), X = i, Y = j});
                    tb.BoardSquares.Add(new BoardSquare(){Value = player.TrackingBoard[i, j].ToString(), X = i, Y = j});
                }
            }
            savePlayer.Boards.Add(gb);
            savePlayer.Boards.Add(tb);
            
            //player ships
            foreach (var someShip in player.Ships)
            {
                var ship = new Ship {Name = someShip.ShipType, Health = someShip.Health};
                foreach (var coordinate in someShip.ShipCoordinates)
                {
                    ship.ShipsLocation.Add(new ShipLocation(){X = coordinate.XCord, Y = coordinate.GetY()});
                }
                savePlayer.Ships.Add(ship);
            }

            return savePlayer;
        }

        public static List<Player> LoadSaveFromDb (int index)
        {
            var ctx = new AppDbContext();

            var result = new List<Player>();
            
            var chosenSave = ctx.Saves.Find(index);
            var playerId = chosenSave.User1Id;
            var computerId = chosenSave.User2Id;

            var optionsSomething = JsonConvert.DeserializeObject<Dictionary<string, int>>(chosenSave.Options);
            foreach (var valueKeyPair in optionsSomething)
            {
                Options.ChangeOption(valueKeyPair.Key, valueKeyPair.Value);
            }

            var playerGb = GetBoard(ctx, "GamingBoard", playerId);
            var playerTb = GetBoard(ctx, "TrackingBoard", playerId);
            
            var computerGb = GetBoard(ctx, "GamingBoard", computerId);
            var computerTb = GetBoard(ctx, "TrackingBoard", computerId);

            var playerShips = GetPlayerShips(ctx, playerId);
            var playerSunkenShips = playerShips.FindAll(c => c.Health == 0);
            
            var computerShips = GetPlayerShips(ctx, computerId);
            var computerSunkenShips = computerShips.FindAll(c => c.Health == 0);

            var player = ctx.Users.Find(playerId).GetDomainPlayer(playerShips, (GamingBoard) playerGb, 
                (TrackingBoard) playerTb, playerSunkenShips);
            
            var computer = ctx.Users.Find(computerId)
                .GetDomainComputer(computerShips, (GamingBoard) computerGb, 
                    (TrackingBoard) computerTb, computerSunkenShips);
            
            result.Add(player);
            result.Add(computer);
            
            return result;
        }

        public static void DeleteSave(int index)
        {
            var ctx = new AppDbContext();
            var save = ctx.Saves.Find(index);
            ctx.Saves.Remove(save);
            ctx.Users.Remove(ctx.Users.Find(save.User1Id));
            ctx.Users.Remove(ctx.Users.Find(save.User2Id));
            ctx.SaveChanges();
        }

        public static void OverwriteSave(int index, Player player, Computer computer)
        {
            var ctx = new AppDbContext();
            var save = ctx.Saves.Find(index);
            ctx.Users.Remove(ctx.Users.Find(save.User1Id));
            ctx.Users.Remove(ctx.Users.Find(save.User2Id));
            save.Date = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            save.User1 = SavePlayer(player);
            save.User2 = SavePlayer(null, computer);
            save.Winner = GetWinner(player, computer);
            ctx.SaveChanges();
        }

        public static List<List<string>> GetGameInformation()
        {
            var ctx = new AppDbContext();
            return ctx.Saves.Select(save => new List<string> {save.SaveId.ToString(), save.Date, save.Winner, save.PlayerName}).ToList();
        }

        private static Boards GetBoard(AppDbContext appDbContext, string boardName ,int id)
        {
            if (boardName.Equals("GamingBoard"))
            {
                return appDbContext.Boards.Where(c => c.UserId == id && c.BoardName == "GamingBoard")
                    .Include(a => a.BoardSquares).First().GetDomainGamingBoard();
            }
            return appDbContext.Boards.Where(c => c.UserId == id && c.BoardName == "TrackingBoard")
                .Include(a => a.BoardSquares).First().GetDomainTrackingBoard();
        }

        private static List<Domain.Ships.Ship> GetPlayerShips(AppDbContext appDbContext, int id)
        {
            var result = new List<Domain.Ships.Ship>();
            
            foreach (var ship in appDbContext.Ships.Where(a => a.UserId == id).Include(s => s.ShipsLocation))
            {
                var currentShip = ship.GetDomainShip();
                result.Add(currentShip);
            }

            return result;
        }
    }
}