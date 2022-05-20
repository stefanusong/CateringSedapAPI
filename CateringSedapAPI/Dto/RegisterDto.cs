using static CateringSedapAPI.Entitties.User;

namespace CateringSedapAPI.Dto
{
    public class RegisterDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public UserRole Role { get; set; }
    }
}
