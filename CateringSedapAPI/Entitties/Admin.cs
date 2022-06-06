
namespace CateringSedapAPI.Entitties
{
    public class Admin : User
    {
        public Admin(string? username, string? password, string? name, UserRole role, string? jobPosition) 
            : base(username, password, name, role)
        {
            JobPosition = jobPosition;
        }

        public string? JobPosition { get; set; }
    }
}
