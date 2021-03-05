using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Services.UserService.Models;
using Application.Services.Interfaces;
using Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MVM.Helpers;


namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        public List<User> _users = new List<User>
        {
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
            }
        };

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserDto Authenticate(string email, string password)
        {

            foreach (var u in _users)
            {
                Console.WriteLine(u.Email);
            }
            
            Console.WriteLine($"{email}");

            var user = _users
                .SingleOrDefault(x => x.Email == email && x.Password == password);

            if (user is null) return null;
            
           var token = generateJwtToken(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.FullName,
                Role = user.Role.ToString(),
                Email = user.Email,
                Token = token
            };
        }
        
        public UserDto GetById(int id)
        {
            return _users
                .Where(x => x.Id == id)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.FullName,
                    Role = x.Role.ToString()
                })
                .SingleOrDefault();
        }
        
        public List<UserDto> GetAll()
        {
            return _users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.FullName,
                    Role = x.Role.ToString()
                })
                .ToList();
        }
        
        
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}