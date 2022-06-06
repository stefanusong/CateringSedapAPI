
namespace CateringSedapAPI.Dto
{
    public class RegisterCustomerDto : RegisterDto
    {
        public RegisterCustomerDto(string? username, string? password, string? name, string? address)
              : base(username, password, name)
        {
            Address = address;
        }

        public string? Address { get; set; }
    }
}
