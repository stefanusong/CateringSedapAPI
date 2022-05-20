using CateringSedapAPI.Context;
using CateringSedapAPI.Dto;
using CateringSedapAPI.Helpers;
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
        public readonly IResponseHelper _responseHelper;

        public AuthController(IConfiguration config, ApplicationContext db, IAuthService authService, IResponseHelper responseHelper)
        {
            _config = config;
            _db = db;
            _authService = authService;
            _responseHelper = responseHelper;
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
                return Ok(_responseHelper.GetSuccessResponse("login successful", token));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto newUser)
        {
            try
            {
                var res = await _authService.Register(newUser);
                if (!string.IsNullOrEmpty(res))
                {
                    return Ok(_responseHelper.GetSuccessResponse("register successful", res));
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }
    }
}
