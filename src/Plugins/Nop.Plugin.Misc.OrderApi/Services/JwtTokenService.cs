using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Nop.Core.Configuration;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.OrderApi.Infrastructure;
using Nop.Services.Authentication;

namespace Nop.Plugin.Misc.OrderApi.Services;

public class JwtTokenService
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenService(AppSettings appSettings)
    {
        _jwtConfig = appSettings.Get<JwtConfig>();
    }

    public string GenerateToken(Customer customer)
    {
        var claims = new List<Claim>();
        if (!string.IsNullOrEmpty(customer.Username))
            claims.Add(new Claim(ClaimTypes.Name, customer.Username, ClaimValueTypes.String, NopAuthenticationDefaults.ClaimsIssuer));

        if (!string.IsNullOrEmpty(customer.Email))
            claims.Add(new Claim(ClaimTypes.Email, customer.Email, ClaimValueTypes.Email, NopAuthenticationDefaults.ClaimsIssuer));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtConfig.ExpirationMinutes)),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
