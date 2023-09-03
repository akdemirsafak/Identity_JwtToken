using Identity_JwtToken.Models;
using Identity_JwtToken.Requests;
using Identity_JwtToken.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_JwtToken.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
    {
        AppUser entity= new()
        {
            Email = request.Email,
            UserName = request.UserName
        };
        var identityResult= await _userManager.CreateAsync(entity,request.Password);
        if (identityResult.Succeeded)
        {
            return Ok(new AddUserResponse() { Id = new Guid(entity.Id), Email = entity.Email, UserName = entity.UserName });
        }
        return BadRequest();
    }
    [HttpGet]
    public async Task<IActionResult> GetUserByEmail(GetUserByEmailModelRequest request)
    {

        AppUser hasUser= await _userManager.FindByEmailAsync(request.Email);
        if (hasUser is not null)
        {
            return Ok(new GetUserByEmailResponse() { Id = new Guid(hasUser.Id), Email = hasUser.Email, UserName = hasUser.UserName });
        }

        return NotFound();
    }
}
