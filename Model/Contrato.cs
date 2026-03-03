using Contratos.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Contrato : ITenantEntity
{
    [Key]
    public required Guid ContratoId { get; set; } 
    public required Guid EmpresaId { get; set; }
    public required Empresa Empresa { get; set; }
    public required Guid TenantId { get; set; }
    public required Tenant Tenant { get; set; }
    public required Guid ContratanteId { get; set; }
    public required Contratante Contratante { get; set; }
    public string? Titulo { get; set; }
    public string? Objeto { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public double Valor { get; set; }
    public ICollection<FormaPagamento>? FormasPagamento { get; set; }
}
