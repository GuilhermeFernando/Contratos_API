using System.ComponentModel.DataAnnotations;

namespace Contratos.Data.Dto.TenantDto;

public class CreateTenantDto
{
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
