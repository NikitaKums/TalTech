namespace DAL
{
    public class ShipLocation
    {
        public int ShipLocationId { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }
        
        public int ShipId { get; set; }
        public Ship Ship { get; set; }
    }
}