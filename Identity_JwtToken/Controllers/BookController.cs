using Identity_JwtToken.DbContext;
using Identity_JwtToken.Models;
using Identity_JwtToken.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity_JwtToken.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly BookDbContext _dbContext;

    public BookController(BookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _dbContext.Books.ToListAsync());
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBookRequest request)
    {
        var entity= new Book()
        {
            Name= request.Name,
            Description= request.Description,
            Price= request.Price
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
