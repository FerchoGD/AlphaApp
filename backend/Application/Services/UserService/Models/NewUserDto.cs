using Domain.Users;

namespace Application.Services.UserService.Models
{
    public class NewUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string PassWord { get; set; }
    }
}