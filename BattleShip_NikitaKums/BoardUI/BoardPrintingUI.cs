﻿using System;
using System.Text;
using Domain.Boards;

namespace BoardUI
{
    public class BoardPrintingUI
    {

        public string GetBothBoards(Boards gamingBoard, Boards trackingBoard)
        {
            var sb = new StringBuilder();
            sb.Append(gamingBoard.BoardName);
            sb.AppendLine();
            sb.Append(GetSingleBoard(gamingBoard));
            sb.AppendLine();
            sb.Append(trackingBoard.BoardName);
            sb.AppendLine();
            sb.Append(GetSingleBoard(trackingBoard));
            return sb.ToString();
        }
        
        public string GetSingleBoard(Boards board)
        {            
            var sb = new StringBuilder();
            
            sb.Append(GetLettersForBoard(board.BoardSize) + Environment.NewLine);
            
            for (var i = 0; i < board.BoardSize; i++)
            {
                sb.Append(GetRowSeparator(board.BoardSize));
                sb.AppendLine();
                sb.Append(GetNumbersForBoard(i));
                for (var j = 0; j < board.BoardSize; j++)
                {
                    sb.Append("| " + GetBoardSquareStateSymbol(board[i, j]) + " ");
                }
                sb.Append("|");
                sb.AppendLine();

            }
            sb.Append(GetRowSeparator(board.BoardSize));
    
            return sb.ToString();
        }
        
        private static string GetRowSeparator(int boardSize)
        {
            var sb = new StringBuilder();
            sb.Append("  ");
            for (var i = 0; i < boardSize; i++)
            {
                sb.Append("+---");
            }
            sb.Append("+");
            return sb.ToString();
        }

        public string GetBoardSquareStateSymbol(BoardSquareState state)
        {
            switch (state)
            {
                case BoardSquareState.Water: return " ";
                case BoardSquareState.Carrier: return "■";
                case BoardSquareState.BattleShip: return "■";
                case BoardSquareState.Submarine: return "■";
                case BoardSquareState.Cruiser: return "■";
                case BoardSquareState.Patrol: return "■";
                case BoardSquareState.Hit: return "x";
                case BoardSquareState.Miss: return "-";
                case BoardSquareState.Neighbour: return " ";
                case BoardSquareState.Dead: return "F";
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public string GetLettersForBoard(int boardSize)
        {
            var sb = new StringBuilder();
            var alignmentHelper = true;
            var character = 'a';
            sb.Append(" ");
            for (var i = 0; i < boardSize; i++)
            {
                if (character == 'z' && alignmentHelper)
                {
                    sb.Append(" ");
                    alignmentHelper = false;
                }
                if (character == 'z')
                {
                    sb.Append(" ");
                    sb.Append(character);
                    sb.Append(i - 15);
                }
                else
                {
                    sb.Append("   ");
                    sb.Append(character);
                    character++;
                }
            }

            //sb.AppendLine();
            return sb.ToString();
        }
        
        private static string GetNumbersForBoard(int i)
        {    
            var sb = new StringBuilder();
            if (i < 10) sb.Append(" " + i);
            else
            {
                sb.Append(i);
            }
            return sb.ToString();
        }
        public string GetBoardsAsString(GamingBoard gamingBoard, TrackingBoard trackingBoard)
        {
            var sb = new StringBuilder();
            sb.Append(GetLettersForBoard(gamingBoard.BoardSize));
            sb.Append("\t");
            sb.Append(GetLettersForBoard(trackingBoard.BoardSize) + Environment.NewLine);

            for (var i = 0; i < gamingBoard.BoardSize; i++)
            {
                sb.Append(GetRowSeparator(gamingBoard.BoardSize) + "\t");
                sb.Append(GetRowSeparator(trackingBoard.BoardSize) + Environment.NewLine);
                sb.Append(GetNumbersForBoard(i));
                
                for (var j = 0; j < gamingBoard.BoardSize + 1; j++)
                {
                    if (j < gamingBoard.BoardSize)
                    {
                        sb.Append("| " + GetBoardSquareStateSymbol(gamingBoard[i, j]) + " ");
                        continue;
                    }

                    if (j == gamingBoard.BoardSize)
                    {
                        sb.Append("|\t");
                        sb.Append(GetNumbersForBoard(i));
                    }

                    for (var k = 0; k < trackingBoard.BoardSize; k++)
                    {
                        sb.Append("| " + GetBoardSquareStateSymbol(trackingBoard[i, k]) + " "); 
                    }
                    
                }                
                sb.Append("|" + Environment.NewLine);
            }

            sb.Append(GetRowSeparator(gamingBoard.BoardSize) + "\t");
            sb.Append(GetRowSeparator(trackingBoard.BoardSize));
            
            return sb.ToString();
        }
    }
}
