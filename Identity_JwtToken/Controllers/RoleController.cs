using Identity_JwtToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity_JwtToken.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Roles ="Admin")]
public class RoleController : ControllerBase
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleController(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromHeader] string name) {
        await _roleManager.CreateAsync(new AppRole() { Name=name});
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _roleManager.Roles.ToListAsync());
    }
}
