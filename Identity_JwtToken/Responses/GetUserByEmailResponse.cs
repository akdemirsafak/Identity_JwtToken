namespace Identity_JwtToken.Responses;

public class GetUserByEmailResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
