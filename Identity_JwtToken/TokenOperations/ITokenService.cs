using Identity_JwtToken.Models;

namespace Identity_JwtToken.TokenOperations;

public interface ITokenService
{
    TokenDto CreateToken(AppUser appUser);
}
