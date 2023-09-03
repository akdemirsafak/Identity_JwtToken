using System.Security.Claims;

namespace Identity_JwtToken.Helper;

public class CurrentUser : ICurrentUser
{
    private IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirstValue(claimType:ClaimTypes.NameIdentifier);
    public string GetUserName => _httpContextAccessor.HttpContext.User.FindFirstValue(claimType:ClaimTypes.Name);
    public string GetEmail => _httpContextAccessor.HttpContext.User.FindFirstValue(claimType:ClaimTypes.Email);

    
}
