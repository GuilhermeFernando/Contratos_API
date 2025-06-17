using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Tenant
{
    [Key]
    [Required]
    public int TenantId { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public DateTime DataCriacao { get; set; }
    [Required]
    public string Email { get; set; }
    public string Ddd { get; set; }
    public string Telefone { get; set; }
    public string UrlLogo { get; set; }
}
