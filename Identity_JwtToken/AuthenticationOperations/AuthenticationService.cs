using Identity_JwtToken.DbContext;
using Identity_JwtToken.Models;
using Identity_JwtToken.Requests;
using Identity_JwtToken.TokenOperations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_JwtToken.AuthenticationOperations;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly BookDbContext _dbContext;

    public AuthenticationService(UserManager<AppUser> userManager, ITokenService tokenService, BookDbContext dbContext)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<TokenDto> LoginAsync(LoginRequest request)
    {

        var hasUser= await _userManager.FindByEmailAsync(request.Email);
        if (hasUser is null)
        {
            return new TokenDto();
        }
        var checkUserPasswordResult= await _userManager.CheckPasswordAsync(hasUser,request.Password);
        if (!checkUserPasswordResult)
        {
            return new TokenDto();
        }
        var token = _tokenService.CreateToken(hasUser); // !


        var userRefreshToken=await _dbContext.UserRefreshTokens.Where(x=>x.UserId==hasUser.Id).SingleOrDefaultAsync();

        if (userRefreshToken is null)
        {
            var newRefreshToken=new UserRefreshToken()
            {
                UserId=hasUser.Id,
                Code=token.RefreshToken,
                Expiration=token.RefreshTokenExpiration

            };
            await _dbContext.AddAsync(newRefreshToken);
        }
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }
        await _dbContext.SaveChangesAsync();
        return token;

    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        AppUser entity= new()
        {
            Email = request.Email,
            UserName = request.UserName
        };
        var identityResult= await _userManager.CreateAsync(entity,request.Password);
        if (identityResult.Succeeded)
        {
           var addtoRole= await _userManager.AddToRoleAsync(entity, "Standart");
        }
    }
}
