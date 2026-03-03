using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class ContratanteDto
{
    [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
    public Guid EmpresaId { get; set; }
    [Required(ErrorMessage = "A empresa é obrigatória.")]   
    public string RazaoSocial { get; set; } = string.Empty;
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public Guid EnderecoId { get; set; }
    [Required(ErrorMessage = "O endereço é obrigatório.")]   
    public string NomeFantasia { get; set; } = string.Empty;
    [Required(ErrorMessage = "O documento é obrigatório.")]
    public string Documento { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ID do tenant é obrigatório.")]
    public Guid TenantId { get; set; } 
}
