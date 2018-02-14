using Microsoft.EntityFrameworkCore;
using LeaderboardCoreWebApp.Models;

namespace LeaderboardCoreWebApp.Data
{
    public class LeaderboardContext : DbContext
    {
        public LeaderboardContext(DbContextOptions<LeaderboardContext> options) : base(options)
        {
        }

        public DbSet<Competitor> Competitors { get; set; }

        public DbSet<Leaderboard> Leaderboards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // renaming DB table from Competitors to Competitor
            modelBuilder.Entity<Competitor>().ToTable("Competitor");
            modelBuilder.Entity<Subscriber>().ToTable("Subscriber");
            modelBuilder.Entity<Leaderboard>().ToTable("Leaderboard");
        }

        public DbSet<Subscriber> Subscribers { get; set; }
    }
}
