using Contratos.Interface;
using Contratos.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Contratos.Services;

public class JwtServices : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime Expiration) GenerateToken(Usuario usuario)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var key = jwtSection.GetValue<string>("Key") ?? throw new InvalidOperationException("JWT Key is not configured.");
        var issuer = jwtSection.GetValue<string>("Issuer") ?? throw new InvalidOperationException("JWT Issuer is not configured.");
        var audience = jwtSection.GetValue<string>("Audience") ?? throw new InvalidOperationException("JWT Audience is not configured.");
        var expirationMinutes = jwtSection.GetValue<int>("ExpirationMinutes");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NomeUsuario),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim("TenantId", usuario.TenantId.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return (tokenString, expiration);
    }
}
