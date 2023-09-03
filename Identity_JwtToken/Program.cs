using Identity_JwtToken.DbContext;
using Identity_JwtToken.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"),
        option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(BookDbContext))!.GetName().Name); });
});

builder.Services.AddIdentity<AppUser, AppRole>()
.AddEntityFrameworkStores<BookDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
