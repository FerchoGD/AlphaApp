using System.Linq;
using Domain.Users;
using Persistence.Context;

namespace Persistence.Data
{
    public class DbInitializer
    {
        public static void Initialize(AlphaContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new []
            {
                new User ("Fercho Administrador", "fercho@gmail.com",Role.Admin, "Lolita"),
                new User ("Pepito destinatario", "destiner@gmail.com",Role.Destinater, "destinater")
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}