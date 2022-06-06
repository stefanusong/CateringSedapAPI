
namespace CateringSedapAPI.Entitties
{
    public class Driver : User
    {
        public Driver(string? username, string? password, string? name, UserRole role, string? bikeNumber) 
            : base(username, password, name, role)
        {
            BikeNumber = bikeNumber;
        }

        public string? BikeNumber { get; set; }
    }
}
