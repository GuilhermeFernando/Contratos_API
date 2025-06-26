using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Data.Dto.ContratoDto;

public class ReadContratoDto
{
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
    public FormaPagamento FormasPagamento { get; set; }

}
