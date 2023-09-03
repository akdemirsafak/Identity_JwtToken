using Identity_JwtToken.Models;
using Identity_JwtToken.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_JwtToken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            AppUser entity= new()
            {
                Email = request.Email,
                UserName = request.UserName
            };
            var identityResult= await _userManager.CreateAsync(entity,request.Password);
            if (identityResult.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var hasUser= await _userManager.FindByNameAsync(request.username);

            if (hasUser is null)
                return BadRequest();

            var checkPasswordResult =await _userManager.CheckPasswordAsync(hasUser, request.password);
            if (!checkPasswordResult)
                return BadRequest();

            return Ok("MyToken"); //Token döneceğiz.
        }
    }
}
