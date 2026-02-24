using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class TenantDto
{
    [Required]
    public Guid TenantId { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório.") ]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data Criacao é obrigatório.")]
    public DateTime DataCriacao { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Ddd { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string UrlLogo { get; set; } = string.Empty;
}
