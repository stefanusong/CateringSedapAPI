using static CateringSedapAPI.Entitties.User;

namespace CateringSedapAPI.Dto
{
    public abstract class RegisterDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public UserRole Role { get; set; }

        protected RegisterDto(string? username, string? password, string? name)
        {
            Username = username;
            Password = password;
            Name = name;
        }
    }
}
