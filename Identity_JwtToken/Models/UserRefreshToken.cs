﻿namespace Identity_JwtToken.Models;

public class UserRefreshToken
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Code { get; set; }
    public DateTime Expiration { get; set; }
}
