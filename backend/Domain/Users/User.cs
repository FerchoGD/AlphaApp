namespace Domain.Users
{
    public class User
    {
        private User()
        { }

        public User(string fullname, string email, Role role, string password)
        {
            FullName = fullname;
            Email = email;
            Role = role;
            Password = password;
        }
        
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
    }
}