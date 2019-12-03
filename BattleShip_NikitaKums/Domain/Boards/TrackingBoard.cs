using System;

namespace Domain.Boards
{
    public class TrackingBoard : Boards
    {
        public TrackingBoard()
        {
            BoardName = "TrackingBoard";
            Board = new BoardSquareState[BoardSize, BoardSize];
            FillBoard(); 
        }
        
        public TrackingBoard(int size)
        {
            BoardName = "TrackingBoard";
            Board = new BoardSquareState[size, size];
        }
        
    }
}