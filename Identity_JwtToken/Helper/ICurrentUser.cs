namespace Identity_JwtToken.Helper;

public interface ICurrentUser
{
    public string GetUserId { get; }
}
