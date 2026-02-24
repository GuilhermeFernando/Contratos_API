using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class ContratoDto
{
    [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
    public Guid EmpresaId { get; set; }
    [Required(ErrorMessage = "A empresa é obrigatória.")]
    public Empresa? Empresa { get; set; }
    [Required(ErrorMessage = "O ID do tenant é obrigatório.")]
    public Guid TenantId { get; set; }
    [Required(ErrorMessage = "O tenant é obrigatório.")]
    public Tenant? Tenant { get; set; }
    [Required(ErrorMessage = "O ID do contratante é obrigatório.")]
    public Guid ContratanteId { get; set; }
    [Required(ErrorMessage = "O contratante é obrigatório.")]
    public Contratante? Contratante { get; set; }
    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Titulo { get; set; } = string.Empty;
    [Required(ErrorMessage = "O objeto é obrigatório.")]
    public string Objeto { get; set; } = string.Empty;
    [Required(ErrorMessage = "A data de início é obrigatória.")]
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public double Valor { get; set; }
    public FormaPagamento? FormasPagamento { get; set; }
}
