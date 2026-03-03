using Contratos.Interface;
using Contratos.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        var key = jwtSection.GetValue<string>("SecretKey") ?? throw new InvalidOperationException("JWT Key is not configured.");
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

    public (string Acesstoken, DateTime AcessTokenExpiration) GenerateAccessToken(Usuario usuario)
    {
        var key = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT não configurado.");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(15);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
            new Claim(ClaimTypes.Name, usuario.NomeUsuario),
            new Claim(ClaimTypes.Email, usuario.Email ?? ""),
            new Claim("TenantId", usuario.TenantId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return(tokenValue, expiration);
    }


    public (string RefreshToken, DateTime RefreshTokenExpiration) GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);
        var expiration = DateTime.UtcNow.AddDays(7);
        return (refreshToken, expiration);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var secretKey = _configuration["Jwt:SecretKey"];
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = false
            
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
         !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
             StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token inválido");
        }
        return principal;        
    }
}
