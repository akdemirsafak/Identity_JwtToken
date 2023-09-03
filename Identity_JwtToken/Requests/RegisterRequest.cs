namespace Identity_JwtToken.Requests;

public record RegisterRequest(string UserName, string Email, string Password);
