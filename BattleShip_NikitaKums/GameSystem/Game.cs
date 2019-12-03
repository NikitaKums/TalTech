using System;
using System.Collections.Generic;
using BoardUI;
using Domain;
using Domain.Boards;
using Helper;
using SaveSystem;

namespace GameSystem
{
    public static class Game
    {
        public static void FullGame()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the BATTLESHIP game!");
            var player = GetPlayerInfo();
            var computer = new Computer();
            var boardPrintingUi =  new BoardPrintingUI();
            if (!GenerateBoards(player, computer, boardPrintingUi)) return;
            GameLoop(player, computer);
        }

        private static Player GetPlayerInfo()
        {
            Console.WriteLine("What is your name?");
            string name;
            
            while (true)
            {
                name = Console.ReadLine();
                if (name != null && name.Trim().Length != 0)
                {
                    break;
                }
                Console.WriteLine("Name cannot be empty! Enter your name again:");
            }
            
            return new Player(name);
        }

        private static bool GenerateBoards(Player player, Player computer, BoardPrintingUI boardPrintingUi)
        {            
            Console.Clear();
            // ship placing
            while (true)
            {
                Console.WriteLine("Place ships yourself (Press A)" +
                                  "\nGenerate a board with ships already placed (Press B)" +
                                  "\nPress 'Q' to quit");
                var choice = Console.ReadLine()?.Trim().ToUpper();
                
                if (choice == null) continue;
                
                if (choice.Equals("A"))
                {
                    if (PlaceShipsWithCoordinates(boardPrintingUi, player)) break;
                    
                }
                if (choice.Equals("B"))
                {
                    if (player.GenerateRandomBoardWithShips()) break;
                }

                if (choice.Equals("Q"))
                {
                    return false;
                }
                Console.WriteLine("\nUnable to place ships. Increase board size or decrease ship amounts and try again.\n");
            }
            //computer always random placement
            computer.GenerateRandomBoardWithShips();
            return true;
        }
        
        public static void GameLoop(Player player, Computer computer, bool isSave = false, int saveIndex = -1)
        {
            var boardPrintingUi = new BoardPrintingUI();
            var done = false;
            var computerWin = false;

            if (player.HitPoints == 0 || computer.HitPoints == 0)
            {
                Console.WriteLine("The game has already been won.");
                HelperMethods.WaitForUser();
                return;
            }
            
            // game moves
            while (!done)
            {
                Console.Clear();
                //Console.WriteLine(Environment.NewLine + boardPrintingUi.GetBoardsAsString(player.GamingBoard, player.TrackingBoard));
                //Console.WriteLine("----------------------------------");
                //Console.WriteLine(boardPrintingUi.GetSingleBoard(computer.GamingBoard));
                
                if (computer.SunkShips.Count > 0)
                {
                    foreach (var sunkShip in computer.SunkShips)
                    {
                        foreach (var coordinate in sunkShip.ShipCoordinates)
                        {
                            player.TrackingBoard[coordinate.GetX(), coordinate.GetY()] = BoardSquareState.Dead;
                        }
                    }
                }
                
                Console.WriteLine(Environment.NewLine + boardPrintingUi.GetBoardsAsString(player.GamingBoard, player.TrackingBoard));
                Console.WriteLine($"Ships that you have sunk this session: {string.Join(", ", computer.SunkShips)}\n");
                Console.WriteLine("player hp " + player.HitPoints + " computer hp " + computer.HitPoints);
                //Player move
                Console.WriteLine($"{player}, pick x coordinate (number):");
                var xCord = Console.ReadLine();
                Console.WriteLine($"{player}, pick y coordinate (letter):");
                var yCord = Console.ReadLine();
                
                if (!Coordinates.ValidateCoordinates(x: xCord, y: yCord))
                {
                    Console.WriteLine("Invalid coordinates. Try again.");
                    continue;
                }

                var playerShotCoordinates = new Coordinates(Convert.ToInt32(xCord), yCord);
                Console.WriteLine($"\nYou shot at x: {playerShotCoordinates.GetX()} y: {playerShotCoordinates.YCord}");
                
                //ParseShot(Coordinates, attacker, defender)
                if (Boards.ParseShot(playerShotCoordinates, player, computer))
                {
                    Console.WriteLine("You hit!");
                    if (computer.HitPoints <= 0)
                    {
                        done = true;
                    }
                }
                else
                {
                    Console.WriteLine("You missed :(");
                } // end player move

                //Computer move
                if (!done)
                {
                    var computerShotCoordinates = computer.MakeMove();
                    var shotResultComputer = Boards.ParseShot(computerShotCoordinates, computer, player);
                
                    Console.WriteLine($"\n{computer.UserName} shot at x: {computerShotCoordinates.GetX()} y: {computerShotCoordinates.YCord}");
                
                    if (shotResultComputer)
                    {
                        Console.WriteLine($"{computer.UserName} hit!");
                        if (player.HitPoints <= 0)
                        {
                            done = true;
                            computerWin = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{computer.UserName} missed :(\n");
                    } // end computer move
                }
                
                Console.WriteLine("Press 'Q' to quit or any other key to continue.");
                var userChoice = Console.ReadLine();

                if (userChoice != null && userChoice.ToLower().Equals("q") || isSave && done)
                {
                    if (isSave)
                    {
                        while (true)
                        {
                            Console.WriteLine("Game has been won!");
                            Console.WriteLine("1 - Overwrite current save | 2 - create new save | Any key to exit");
                            var saveAction = Console.ReadLine();
                            if (saveAction != null && saveAction.ToLower().Equals("1"))
                            {
                                GameSaveRead.OverwriteSave(saveIndex, player, computer);
                                Console.WriteLine("Save overwritten.");
                                return;
                            }

                            if (saveAction != null && !saveAction.ToLower().Equals("2")) return;
                            GameSaveRead.Save(player, computer);
                            Console.WriteLine("Game saved.");
                            HelperMethods.WaitForUser();
                            return;
                        }
                    }

                    AskForGameSave(player, computer);
                    HelperMethods.WaitForUser();
                    return;
                }
            }

            Console.WriteLine(computerWin ? $"\n{computer.UserName} won this game." : "\nYou won this game!!");
            Console.WriteLine("Thank you for playing!\n" +
                              "You can press Q to view opponent's board.");
            var showOpponentBoard = Console.ReadLine()?.ToLower();
            
            if (showOpponentBoard != null && showOpponentBoard.Equals("q"))
            {
                Console.WriteLine(boardPrintingUi.GetSingleBoard(computer.GamingBoard));
            }
            
            AskForGameSave(player, computer);
            HelperMethods.WaitForUser();
        }

        private static void AskForGameSave(Player player, Computer computer)
        {
            Console.WriteLine("Would you like to save current game? Y/N");
            var saveGame = Console.ReadLine();
            if (saveGame == null || !saveGame.ToLower().Equals("y")) return;
            GameSaveRead.Save(player, computer);
            Console.WriteLine("\nGame has been saved.");
        }

        private static bool PlaceShipsWithCoordinates(BoardPrintingUI boardPrinting, Player player)
        {
            var coordinatesList = new List<Coordinates>();
             foreach (var ship in player.Ships)
            {
                Console.WriteLine(boardPrinting.GetSingleBoard(player.GamingBoard));
                coordinatesList.Clear();
                var done = false;
                while (!done)
                {
                    var trackKeeper = 0;
                    coordinatesList.Clear();
                    var previousCoordinateX = "";
                    var previousCoordinateY = "";
                    Console.WriteLine("Coordinates for " + ship + ". Enter coordinates: ");
                    while (trackKeeper < ship.Size)
                    {
                        Console.WriteLine($"Enter coordinate x ({trackKeeper}): ");
                        var tempX = Console.ReadLine();
                        
                        Console.WriteLine($"Enter coordinate y ({trackKeeper}): ");
                        var tempY = Console.ReadLine();

                        if (tempX == previousCoordinateX && tempY == previousCoordinateY || !Coordinates.ValidateCoordinates(tempX, tempY))
                        {
                            Console.WriteLine("Invalid coordinates. Try again.");
                            continue;
                        }

                        previousCoordinateX = tempX;
                        previousCoordinateY = tempY;
                        var x = int.Parse(tempX);
                        coordinatesList.Add(new Coordinates(x, tempY));
                        trackKeeper++;
                    }

                    if (!player.GamingBoard.SetShipUsingCoordinates(coordinatesList, ship))
                    {
                        Console.WriteLine("Could not set ship at given coordinates.\nPress 'Q' to quit or 'ENTER' to try again.");
                        var e = Console.ReadLine()?.ToUpper();
                        if (e != null && e.Equals("Q")) return false;
                        continue;
                    }
                    done = true;
                }
            }
            player.GamingBoard.RemoveNeighbours(player.GamingBoard.BoardSize);
            return true;
        }

        public static void SelectGameAndStart()
        {
            Console.Clear();
            Console.WriteLine("Fetching saves ..");
            var listOfHeaders = GameSaveRead.GetGameInformation();
            Console.Clear();
            // no saves
            if (listOfHeaders.Count == 0)
            {
                Console.WriteLine("No saves found.");
                HelperMethods.WaitForUser();
                return;
            }
            // print header
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{"Index", -10} {"Save Date",-25} {"Winner",-15} {"Player",-5}");
            Console.ResetColor();
            // print saves
            var availableIndexes = new List<int>();
            foreach (var temp in listOfHeaders)
            {
                availableIndexes.Add(int.Parse(temp[0]));
                Console.Write($"{temp[0], -10} {temp[1],-25} {temp[2],-15} {temp[3],-5} \n");
            }
            // choosing a save and validation
            Console.WriteLine("\nChoose a save by index. Or press 'Q' to quit.");
            int userChoice;
            while (true)
            {
                var userInput = Console.ReadLine();
                if (userInput != null && userInput.ToLower().Equals("q")) return;
                if (!int.TryParse(userInput, out userChoice) || !availableIndexes.Contains(userChoice))
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }
                break;
            }
            // actions with save
            Console.WriteLine($"\nChosen save: {userChoice}\nAvailable actions:");
            Console.WriteLine("1 Continue | 2 Watch replay | 3 Delete");
            int saveAction;
            while (true)
            {
                var userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out saveAction) || saveAction != 1 && saveAction != 2 && saveAction != 3)
                {
                    Console.WriteLine("Choose again.");
                    continue;
                }

                break;
            }

            switch (saveAction)
            {
                case 1:
                    var temp = GameSaveRead.LoadSaveFromDb(userChoice);
                    GameLoop(temp[0], (Computer) temp[1], true, userChoice);
                    break;
                case 2:
                    ReplayGame(GameSaveRead.LoadSaveFromDb(userChoice));
                    break;
                case 3:
                    GameSaveRead.DeleteSave(userChoice);
                    Console.WriteLine("Save deleted.");
                    HelperMethods.WaitForUser();
                    break;
                default:
                    Console.WriteLine("No such option");
                    break;
            }
            
        }

        private static void ReplayGame(List<Player> players)
        {
            var oldPlayer = players[0];
            var oldComputer = players[1];
            
            var newPlayer = new Player(oldPlayer.UserName);
            var newComputer = new Computer();

            newPlayer.GamingBoard = RegenerateOgBoards(oldPlayer);
            newComputer.GamingBoard = RegenerateOgBoards(oldComputer);

            var bpu = new BoardPrintingUI();

            for (var i = 0; i < oldPlayer.MovesDoneList.Count; i++)
            {
                Console.Clear();
                var playerShot = oldPlayer.MovesDoneList[i];
                Console.WriteLine($"{oldPlayer.UserName} shot at ({i}): x: {playerShot.XCord} y: {playerShot.YCord}");
                Boards.ParseShot(playerShot, newPlayer, newComputer, true);
                Console.WriteLine("Player boards: \n" + bpu.GetBoardsAsString(newPlayer.GamingBoard, newPlayer.TrackingBoard));

                if (oldPlayer.MovesDoneList.Count > oldComputer.MovesDoneList.Count && i == oldPlayer.MovesDoneList.Count - 1) break;

                var computerShot = oldComputer.MovesDoneList[i];
                Console.WriteLine($"Computer {oldComputer.UserName} shot at ({i}): x: {computerShot.XCord} y: {computerShot.YCord}");
                Boards.ParseShot(computerShot, newComputer, newPlayer, true);
                Console.WriteLine("===============");
                Console.WriteLine("Computer boards: \n" + bpu.GetBoardsAsString(newComputer.GamingBoard, newComputer.TrackingBoard));
                HelperMethods.WaitForUser();
            }
            Console.WriteLine("That are all the moves done in this save.");
            HelperMethods.WaitForUser();
        }

        private static GamingBoard RegenerateOgBoards(Player savePlayer)
        {
            var size = savePlayer.GamingBoard.BoardSize;
            var newGamingBoard = new GamingBoard(size);

            foreach (var ship in savePlayer.Ships)
            {
                foreach (var coordinate in ship.ShipCoordinates)
                {
                    newGamingBoard[coordinate.GetX(), coordinate.GetY()] = ship.BoardSquareState;
                }
            }
            
            return newGamingBoard;
        }
        
    }
}