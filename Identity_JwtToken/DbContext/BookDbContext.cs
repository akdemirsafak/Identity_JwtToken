using Identity_JwtToken.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity_JwtToken.DbContext;

public class BookDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<AppRole> Roles { get; set; }
    public DbSet<Book> Books { get; set; }
}
