using System;

namespace Domain.Boards
{
    [Serializable]
    public class GamingBoard : Boards
    {        
        public GamingBoard()
        {
            BoardName = "GamingBoard";
            Board = new BoardSquareState[BoardSize,BoardSize];
            FillBoard(); 
        }

        public GamingBoard(int size)
        {
            BoardName = "GamingBoard";
            Board = new BoardSquareState[size, size];
        }
    }
}