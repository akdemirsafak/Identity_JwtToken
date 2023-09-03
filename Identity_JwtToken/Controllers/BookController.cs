using Identity_JwtToken.DbContext;
using Identity_JwtToken.Helper;
using Identity_JwtToken.Models;
using Identity_JwtToken.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Identity_JwtToken.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly BookDbContext _dbContext;
    private readonly ICurrentUser _currentUser;
    public BookController(BookDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _dbContext.Books.ToListAsync());
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] AddBookRequest request)
    {
        var entity= new Book()
        {
            Name= request.Name,
            Description= request.Description,
            Price= request.Price,
            CreatedBy=new Guid(HttpContext.User.FindFirstValue(claimType:ClaimTypes.NameIdentifier))
        };
        await _dbContext.Books.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity= await _dbContext.Books.FindAsync(id);
        _dbContext.Books.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
