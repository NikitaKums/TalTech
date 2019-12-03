using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {

        public DbSet<Save> Saves { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardSquare> BoardSquares { get; set; }
        public DbSet<ShipLocation> ShipLocations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseMySQL("server=alpha.akaver.com;database=student2018_179097;user=student2018;password=student2018");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardSquare>().HasOne<Board>(b => b.Board).WithMany(s => s.BoardSquares)
                .HasForeignKey(x => x.BoardId);
        }
    }
}
