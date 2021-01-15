using Microsoft.EntityFrameworkCore;
using TreasureTrack.Data.Entities;

namespace TreasureTrack.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        //public DbSet<Puzzle> Puzzles { get; set; }
        //public DbSet<Attempt> Attempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}