using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Usuario
{
    [Key]
    public required Guid UsuarioId { get; set; }
    public required string NomeUsuario { get; set; }
    public required string Senha { get; set; }
    public string Email { get; set; } = string.Empty;    
    public  string Telefone { get; set; } = string.Empty;
    public required Guid TenantId { get; set; }
    public required Tenant Tenant { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
