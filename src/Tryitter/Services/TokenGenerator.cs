using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tryitter.Models;

namespace Tryitter.Services;

public class TokenGenerator : ITokenGenerator
{
    public IConfiguration _configuration { get; }
    
    public TokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
       
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = AddClaims(user),
            // SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("TokenConstants.Secret")), SecurityAlgorithms.HmacSha256Signature),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Secret"])), SecurityAlgorithms.HmacSha256Signature),
            Expires = DateTime.Now.AddDays(1)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity AddClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim("Name", user.Name));
       
        claims.AddClaim(new Claim("Status", user.Status));
        return claims;
    }
}