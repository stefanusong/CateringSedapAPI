using static CateringSedapAPI.Entitties.User;

namespace CateringSedapAPI.Dto
{
    public class RegisterAdminDto : RegisterDto
    {
        public RegisterAdminDto(string? username, string? password, string? name, string? jobPosition)
            : base(username, password, name)
        {
            JobPosition = jobPosition;
        }
        public string? JobPosition { get; set; }
    }
}
