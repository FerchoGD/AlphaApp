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
    }
}