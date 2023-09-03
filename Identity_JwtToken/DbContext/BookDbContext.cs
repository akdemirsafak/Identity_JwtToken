using Identity_JwtToken.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity_JwtToken.DbContext;

public class BookDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}
