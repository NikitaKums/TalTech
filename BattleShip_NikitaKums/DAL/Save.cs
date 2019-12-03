namespace DAL
{
    public class Save
    {
        public int SaveId { get; set; }
        
        public string Date { get; set; }
        public string PlayerName { get; set; }
        public string Winner { get; set; }
        public string Options { get; set; }
        
        public int User1Id { get; set; }
        public User User1 { get; set; }
        
        public int User2Id { get; set; }
        public User User2 { get; set; }

    }
}