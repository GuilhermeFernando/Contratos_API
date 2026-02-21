using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class EmpresaDto
{
    public Guid EnderecoId { get; set; }
   
    [Required]
    public Guid UsuarioId { get; set; }

    [Required]
    public Guid TenantId { get; set; }
    [Required] 
    public string RazaoSocial { get; set; }
    [Required]
    public string CNPJ { get; set; }
    public string NomeFantasia { get; set; }
    public string IE { get; set; }
    public string IM { get; set; }
    public string NaturezaJuridica { get; set; }
    public DateTime DataAbertura { get; set; }
}
