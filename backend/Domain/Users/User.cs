namespace Domain.Users
{
    public class User
    {
        private User()
        { }

        public User(int tenantId)
        {
            TenantId = tenantId;
        }
        
        public int TenantId { get; set; }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
    }
}