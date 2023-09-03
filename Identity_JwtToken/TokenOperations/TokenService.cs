﻿using Identity_JwtToken.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity_JwtToken.TokenOperations;

public class TokenService : ITokenService
{
    private readonly TokenOptions _tokenOptions;
    private readonly UserManager<AppUser> _userManager;

    public TokenService(IOptions<TokenOptions> tokenOptions, UserManager<AppUser> userManager)
    {
        _tokenOptions = tokenOptions.Value;
        _userManager = userManager;
    }

    public TokenDto CreateToken(AppUser appUser)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer:_tokenOptions.Issuer,
            //audience: _tokenOptions.Audience,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: GetClaims(appUser,_tokenOptions.Audience),
            signingCredentials: signingCredentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);
        var tokenDto = new TokenDto
        {
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            AccessTokenExpiration = accessTokenExpiration,
            RefreshTokenExpiration = refreshTokenExpiration
        };
        return tokenDto;
    }

    private string CreateRefreshToken()
    {
        var numberByte = new byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }
    private  IEnumerable<Claim> GetClaims(AppUser appUser, List<String> audiences)
    {
        var roles= _userManager.GetRolesAsync(appUser).Result;
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, appUser.Id),
            new(JwtRegisteredClaimNames.Email, appUser.Email),
            new(ClaimTypes.Name, appUser.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));


        return claims;
    }

}
