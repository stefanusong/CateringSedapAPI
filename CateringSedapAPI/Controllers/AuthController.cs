using CateringSedapAPI.Context;
using CateringSedapAPI.Dto;
using CateringSedapAPI.Factories;
using CateringSedapAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CateringSedapAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _config;
        public readonly ApplicationContext _db;
        public readonly IAuthService _authService;
        public readonly IResponseFactory _responseFactory;

        public AuthController(IConfiguration config, ApplicationContext db, IAuthService authService, IResponseFactory responseFactory)
        {
            _config = config;
            _db = db;
            _authService = authService;
            _responseFactory = responseFactory;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            try
            {
                var token = await _authService.Login(user);
                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(_responseFactory.GetSuccessResponse("login successful", token));
            }
            catch (Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        [Route("register-customer")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerDto newUser)
        {
            try
            {
                var res = await _authService.Register(newUser);
                if (!string.IsNullOrEmpty(res))
                {
                    return Ok(_responseFactory.GetSuccessResponse("register successful", res));
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminDto newUser)
        {
            try
            {
                var res = await _authService.Register(newUser);
                if (!string.IsNullOrEmpty(res))
                {
                    return Ok(_responseFactory.GetSuccessResponse("register successful", res));
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        [Route("register-driver")]
        public async Task<IActionResult> RegisterDriver(RegisterDriverDto newUser)
        {
            try
            {
                var res = await _authService.Register(newUser);
                if (!string.IsNullOrEmpty(res))
                {
                    return Ok(_responseFactory.GetSuccessResponse("register successful", res));
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }
    }
}
