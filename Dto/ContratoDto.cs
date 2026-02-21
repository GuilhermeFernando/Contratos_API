using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class ContratoDto
{
    public Guid EmpresaId { get; set; }
    [Required]
    public Empresa Empresa { get; set; }
    [Required]
    public Guid TenantId { get; set; }
    [Required]
    public Tenant Tenant { get; set; }
    [Required]
    public Guid ContratanteId { get; set; }
    public Contratante Contratante { get; set; }
    public string Titulo { get; set; }
    public string Objeto { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public double Valor { get; set; }
    public FormaPagamento FormasPagamento { get; set; }
}
