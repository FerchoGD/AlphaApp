using System.Threading;
using Domain.Communications;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.MvmContext
{
    public class MvmContext : DbContext
    {
        public MvmContext(DbContextOptions<MvmContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(1)
                    {
                        Id = 1,
                        FullName = "Fernando Administrador",
                        Email = "fercho@gmail.com",
                        Role = Role.Admin,
                        Password = "lolita"
                    },
                new User(1)
                    {
                        Id = 2,
                        FullName = "Usuario Manager",
                        Email = "manager@gmail.com",
                        Role = Role.Manager,
                        Password = "manager@12345"
                    },
                new User(1)
                    {
                        Id = 3,
                        FullName = "Usuario consulta",
                        Email = "consulta@gmail.com",
                        Role = Role.Destinater,
                        Password = "consulta@12345"
                    });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<User> Users { get; set; }

    }
}