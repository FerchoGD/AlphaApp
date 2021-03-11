using Domain.Communications;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class AlphaContext : DbContext
    {
        public AlphaContext(DbContextOptions<AlphaContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Communication> Communications { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Communication>().ToTable("Communications");
        }
    }
}