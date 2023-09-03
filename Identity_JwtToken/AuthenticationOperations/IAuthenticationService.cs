using Identity_JwtToken.Requests;
using Identity_JwtToken.TokenOperations;

namespace Identity_JwtToken.AuthenticationOperations;

public interface IAuthenticationService
{
    Task<TokenDto> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterRequest request);
}
