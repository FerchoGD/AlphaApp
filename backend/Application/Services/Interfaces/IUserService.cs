using System.Collections.Generic;
using Application.Services.UserService.Models;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(string email, string password);
        UserDto GetById(int id);
        List<UserDto> GetAll();
    }
}