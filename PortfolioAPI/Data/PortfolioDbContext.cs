using Microsoft.EntityFrameworkCore;
using PortfolioShared.Models;

namespace PortfolioAPI.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        { }
            public DbSet<User> Users { get; set; }
            public DbSet<Project> Projects { get; set; }
            public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Skills)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);
        }
    }
}
