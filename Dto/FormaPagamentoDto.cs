using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class FormaPagamentoDto
{
    [Required]
    public string Descricao { get; set; }
    [Required]
    public int NumeroParcela { get; set; }
    [Required]
    public bool Ativo { get; set; } = true;
    [Required]
    public DateTime DataCriacao { get; set; }
    [Required]
    public DateTime DataAlteracao { get; set; }
    public int ContratoId { get; set; }
    public Contrato Contrato { get; set; }

}
