using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Tenant
{
    [Key]
    public required Guid TenantId { get; set; }
    public required string Nome { get; set; }
    public required DateTime DataCriacao { get; set; } 
    public required string Email { get; set; }
    public required string Ddd { get; set; }
    public required string Telefone { get; set; }
    public required string UrlLogo { get; set; }
}
