using Contratos.Model;

namespace Contratos.Interface;

public interface IJwtService
{
    (string Token, DateTime Expiration) GenerateToken(Usuario usuario);
    (string Acesstoken, DateTime AcessTokenExpiration) GenerateAccessToken(Usuario usuario);
    (string RefreshToken, DateTime RefreshTokenExpiration) GenerateRefreshToken();
    System.Security.Claims.ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
