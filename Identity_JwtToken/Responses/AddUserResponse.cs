namespace Identity_JwtToken.Responses;

public class AddUserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
