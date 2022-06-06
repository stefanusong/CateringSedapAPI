using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CateringSedapAPI.Dto;
using CateringSedapAPI.Entitties;
using CateringSedapAPI.Factories;
using CateringSedapAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using static CateringSedapAPI.Entitties.User;

namespace CateringSedapAPI.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto newUser);
        Task<string> Login(LoginDto user);
    }

    public class AuthService : IAuthService
    {
        public readonly IUserRepository _repo;
        public readonly IConfiguration _config;

        public AuthService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<string> Login(LoginDto user)
        {
            // check if user exists and password is correct
            var userFromDb = await _repo.GetUserByUsername(user.Username!);
            if (userFromDb is null)
            {
                throw new Exception("user not found");
            }
            else if (!BCrypt.Net.BCrypt.Verify(user.Password!, userFromDb.Password))
            {
                throw new Exception("password incorrect");
            }

            // generate token
            var token = GenerateToken(userFromDb.Id, userFromDb.Username!);
            return token;
        }

        public async Task<string> Register(RegisterDto newUser)
        {
            await ValidateUser(newUser);

            try
            {
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                User? userEntity = MapUserFromDto(newUser);
                if (userEntity != null)
                {
                    var id = await _repo.CreateUser(userEntity!);
                    return id.ToString();
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task ValidateUser(RegisterDto newUser)
        {
            if (IsNewUserHasEmptyField(newUser))
            {
                throw new Exception("one or more fields are empty");
            }

            if (!IsValidPassword(newUser.Password!))
            {
                throw new Exception("password length must be at least 5 characters and contains a number");
            }

            if (await IsUsernameExists(newUser.Username!))
            {
                throw new Exception("username is already taken");
            }
        }

        private string GenerateToken(Guid userId, string username)
        {
            // create claims details based on the user information
            var authClaims = new List<Claim>
                {
                    new Claim("username", username),
                    new Claim("userId", userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            // sign and create token
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        private async Task<bool> IsUsernameExists(string username)
        {
            return await _repo.GetUserByUsername(username) is not null;
        }

        private static bool IsNewUserHasEmptyField(RegisterDto newUser)
        {
            return string.IsNullOrEmpty(newUser.Username) || string.IsNullOrEmpty(newUser.Password) || string.IsNullOrEmpty(newUser.Name);
        }

        private static bool IsValidPassword(string password)
        {
            return password.Length >= 5 && password.Any(char.IsDigit);
        }

        private static User? MapUserFromDto(RegisterDto dto)
        {
            UserFactory userFactory = new UserFactory();
            switch(dto.Role)
            {
                case UserRole.customer:
                    var customerDto = (RegisterCustomerDto) dto;
                    return userFactory.CreateCustomer(customerDto.Username!, customerDto.Password!, customerDto.Name!,  customerDto.Address!);
                case UserRole.admin:
                    var adminDto = (RegisterAdminDto)dto;
                    return userFactory.CreateAdmin(adminDto.Username!, adminDto.Password!, adminDto.Name!, adminDto.JobPosition!);
                case UserRole.driver:
                    var driverDto = (RegisterDriverDto)dto;
                    return userFactory.CreateDriver(driverDto.Username!, driverDto.Password!, driverDto.Name!, driverDto.BikeNumber!);
            }

            return null;
        }
    }
}
