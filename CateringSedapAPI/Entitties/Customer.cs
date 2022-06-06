

namespace CateringSedapAPI.Entitties
{
    public class Customer : User
    {
        public Customer(string? username, string? password, string? name, UserRole role, string? address) 
            : base(username, password, name, role)
        {
            Address = address;
        }

        public string? Address { get; set; }
    }
}
