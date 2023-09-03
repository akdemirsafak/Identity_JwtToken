using Identity_JwtToken.AuthenticationOperations;
using Identity_JwtToken.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Identity_JwtToken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _authenticationService.RegisterAsync(request);
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _authenticationService.LoginAsync(request));
        }
    }
}
