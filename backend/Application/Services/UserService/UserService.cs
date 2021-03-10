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
using Persistence.Context;


namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AlphaContext _context;
        private readonly AppSettings _appSettings;

        public UserService(AlphaContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        
        public UserDto Authenticate(string email, string password)
        {
            var user = _context.Users
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
            return _context.Users
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
            return _context.Users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.FullName,
                    Role = x.Role.ToString()
                })
                .ToList();
        }

        public User Create(NewUserDto data)
        {
            var newUser = new User(data.FullName, data.Email, data.Role, data.PassWord);

            _context.Add(newUser);
            _context.SaveChanges();

            return newUser;
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