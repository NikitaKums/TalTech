using System;
using BoardUI;
using Domain;
using Domain.Boards;
using Domain.Ships;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void TestShipHealth()
        {
            var carrier = new Carrier();
            carrier.Health = carrier.Health - 1;
            int expected = 4;
            Assert.AreEqual(expected, carrier.Health);
        }

        [Test]
        public void TestCoordinates()
        {
            var cord = new Coordinates(verticalX:1, horizontalY:"c");
            Assert.AreEqual(2, cord.GetY());
            Assert.AreEqual(1, cord.GetX());
        }

        [Test]
        public void TestGamingBoard()
        {
            Options.SetDefaultOptions();
            var gamingBoard = new GamingBoard();
            Assert.AreEqual("GamingBoard", gamingBoard.CurrentBoardName());
            Assert.AreEqual(BoardSquareState.Water, gamingBoard[0,0]);
            gamingBoard[1, 1] = BoardSquareState.Carrier;
            Assert.AreEqual(BoardSquareState.Carrier, gamingBoard[1,1]);
        }
        
        [Test]
        public void TestTrackingBoard()
        {
            Options.SetDefaultOptions();
            var trackingBoard = new TrackingBoard();
            Assert.AreEqual("TrackingBoard", trackingBoard.CurrentBoardName());
            Assert.AreEqual(BoardSquareState.Water, trackingBoard[9,9]);
            trackingBoard[2, 5] = BoardSquareState.Submarine;
            Assert.AreEqual(BoardSquareState.Submarine, trackingBoard[2, 5]);
        }

        [Test]
        public void TestPlayerHealth()
        {
            var player = new Player("kati");
            Assert.AreEqual(15, player.GetHitpoints()); //15 is sum of all ship healths
        }

        [Test]
        public void TestOptionsChanging()
        {
            Options.SetDefaultOptions();
            var board = new TrackingBoard();
            Assert.AreEqual(10, board.BoardSize);
            
            Options.ChangeOption("Board size", 30);
            
            var board1 = new TrackingBoard();
            Assert.AreEqual(30, board1.BoardSize);
            
            var expected = Options.ChangeOption("Board size", 0);
            Assert.AreEqual(expected, false);
        }

        /*[Test]
        public void TestBoardPrinting()
        {
            Options.SetDefaultOptions();
            Options.ChangeOption("Board size", 1); //board size 1x1
            
            var trackingBoard = new TrackingBoard();
            var gamingBoard = new GamingBoard();
            var ui = new BoardPrintingUI();
            
            const string expected = "GamingBoard\r\n" + "    a\r\n" + "  +---+\r\n" + " 0|   |\r\n" + "  +---+\r\n" + 
                                    "TrackingBoard\r\n" + "    a\r\n" + "  +---+\r\n" + " 0|   |\r\n" + "  +---+";
            Assert.AreEqual(expected, ui.GetBothBoards(gamingBoard, trackingBoard));
        }*/
        
        [Test]
        public void TestCoordinatesValidator()
        {
            Options.SetDefaultOptions();
            
            Assert.IsTrue(Coordinates.ValidateCoordinates("9", "j"));
            Assert.IsTrue(Coordinates.ValidateCoordinates("5", "g"));
            Assert.IsTrue(Coordinates.ValidateCoordinates("1", "b"));
            Assert.IsTrue(Coordinates.ValidateCoordinates("0", "a"));
            
            Assert.IsFalse(Coordinates.ValidateCoordinates("10", "j"));
            Assert.IsFalse(Coordinates.ValidateCoordinates("11", "j"));
            Assert.IsFalse(Coordinates.ValidateCoordinates("15", "k"));
            Assert.IsFalse(Coordinates.ValidateCoordinates("1000", "a"));
                
            Options.OPTIONS["Board size"] = 40;
            Assert.IsTrue(Coordinates.ValidateCoordinates("39", "z24"));
            Assert.IsTrue(Coordinates.ValidateCoordinates("0", "a"));
            Assert.IsTrue(Coordinates.ValidateCoordinates("29", "k"));

            Assert.IsFalse(Coordinates.ValidateCoordinates("40", "z25"));
            Assert.IsFalse(Coordinates.ValidateCoordinates("1", "a56"));
            Assert.IsFalse(Coordinates.ValidateCoordinates("1", "z96"));
        }

        [Test]
        public void TestIntToYCharCoordinates()
        {
            Assert.AreEqual("y" ,Coordinates.IntToYCoordinate(24));
            Assert.AreEqual("z10",Coordinates.IntToYCoordinate(25));
            Assert.AreEqual("a",Coordinates.IntToYCoordinate(0));
            Assert.AreEqual("f",Coordinates.IntToYCoordinate(5));
            Assert.AreEqual("z20",Coordinates.IntToYCoordinate(35));


        }

    }
}
