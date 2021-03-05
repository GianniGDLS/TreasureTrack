using Microsoft.EntityFrameworkCore;
using TreasureTrack.Data.Entities;
using TreasureTrack.Data.Entities.Configurations;

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
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Attempt> Attempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}