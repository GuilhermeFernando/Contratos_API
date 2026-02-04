using Contratos.Model;

namespace Contratos.Interface;

public interface IJwtService
{
    (string Token, DateTime Expiration) GenerateToken(Usuario usuario);
}
