namespace FinanceApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public string? Phone { get; set; }
        public virtual ICollection<Account> Accounts { get; set; } = [];
        public User() { }
        public User(string name, string email, string password, string? phone)
        {
            var id = Guid.NewGuid();

            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void PromoteToAdmin() => IsAdmin = true;
    }
}