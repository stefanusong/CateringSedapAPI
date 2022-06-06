
namespace CateringSedapAPI.Dto
{
    public class RegisterDriverDto : RegisterDto
    {
        public RegisterDriverDto(string? username, string? password, string? name, string? bikeNumber)
            : base(username, password, name)
        {
            BikeNumber = bikeNumber;
        }

        public string? BikeNumber { get; set; }
    }
}
