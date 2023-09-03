namespace Identity_JwtToken.TokenOperations;

public class TokenOptions
{
    public List<string> Audience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}
