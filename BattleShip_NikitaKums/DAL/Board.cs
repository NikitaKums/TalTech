using System.Collections.Generic;
using Domain.Boards;

namespace DAL
{
    public class Board
    {
        public int BoardId { get; set; }
        
        public int Size { get; set; }
        public string BoardName { get; set; }
        
        public virtual List<BoardSquare> BoardSquares { get; set; } = new List<BoardSquare>();
        
        public int UserId { get; set; }
        public User User { get; set; }

        public GamingBoard GetDomainGamingBoard()
        {
            var gamingBoard = new GamingBoard(Size);
            foreach (var square in BoardSquares)
            {
                gamingBoard[square.X, square.Y] = gamingBoard.BoardSquareStateFromString(square.Value);
            }

            return gamingBoard;
        }

        public TrackingBoard GetDomainTrackingBoard()
        {
            var trackingBoard = new TrackingBoard(Size);
            foreach (var square in BoardSquares)
            {
                trackingBoard[square.X, square.Y] = trackingBoard.BoardSquareStateFromString(square.Value);
            }

            return trackingBoard;
        }
    }
}