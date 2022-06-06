using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Entitties
{
    [Index(nameof(Username), IsUnique = true)]
    public abstract class User
    {
        protected User(string? username, string? password, string? name, UserRole role)
        {
            Id = new Guid();
            Username = username;
            Password = password;
            Name = name;
            Role = role;
        }

        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public UserRole Role { get; set; }

        public enum UserRole
        {
            customer = 1,
            admin,
            driver
        }
    }
}
