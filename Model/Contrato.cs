using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Contrato
{
    [Key]
    [Required]
    public int ContratoId { get; set; }
    [Required]
    public int EmpresaId { get; set; }
    [Required]
    public Empresa Empresa { get; set; }
    [Required]
    public int TenantId { get; set; }
    [Required]
    public Tenant Tenant { get; set; }
    [Required]
    public int ContratanteId { get; set; }
    public Contratante Contratante { get; set; }
    public string Titulo { get; set; }
    public string Objeto { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public double Valor { get; set; }
    public ICollection<FormaPagamento> FormasPagamento { get; set; } = new List<FormaPagamento>();

}
