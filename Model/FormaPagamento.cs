using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class FormaPagamento
{
    [Key]
    public required Guid FormaPagamentoId { get; set; }
 
    public required string Descricao { get; set; }

    public required int NumeroParcela { get; set; } = 0;
    public required bool Ativo { get; set; } = true;

    public required DateTime DataCriacao { get; set; }

    public required DateTime DataAlteracao { get; set; }
    public required Guid ContratoId { get; set; }
    public Contrato? Contrato { get; set; }


}
