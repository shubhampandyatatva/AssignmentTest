using Assignment.Repository.Data;
using Assignment.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Assignment.Service.Implementations;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;
    private readonly IRolePermissionsService _rolePermissionsService;

    public JWTService(IConfiguration configuration, IRolePermissionsService rolePermissionsService)
    {
        _configuration = configuration;
        _rolePermissionsService = rolePermissionsService;
    }
    
    public async Task<string> GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Roleid.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetClaimValue(string token, string claimType)
    {
        ClaimsPrincipal claimsPrincipal = ValidateToken(token);
        if(claimsPrincipal == null)
        {
            return null;
        }
        Claim claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == claimType);

        return claim?.Value;
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        return new ClaimsPrincipal(new ClaimsIdentity(jsonToken.Claims));
    }
}