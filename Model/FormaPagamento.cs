using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class FormaPagamento
{
    [Key]
    [Required]
    public int FormaPagamentoId { get; set; }
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
