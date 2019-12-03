namespace DAL
{
    public class BoardSquare
    {
        public int BoardSquareId { get; set; }
        
        public string Value { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }
        
        public int BoardId { get; set; }
        public Board Board { get; set; }

    }
}