using Application.Abstractions.Services;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.JWTToken;

public class JWTTokenProvider : IJWTTokenProvider
{
    public string CreateToken(User user)
    {
        var config = ConfigurationGetter.BuildConfiguration();

        List<Claim> claims = new List<Claim>{
                    new Claim("userId", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim(ClaimTypes.Role, user.Role!.Name)};

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Key"],
            audience: config["Jwt:Key"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: cred
        );

        var from = token.ValidFrom;
        var to = token.ValidTo;

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}