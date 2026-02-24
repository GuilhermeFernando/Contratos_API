using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class FormaPagamentoDto
{
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public Guid FormaPagamentoId { get; set; }
    [Required(ErrorMessage = "O ID do endereço é obrigatório.") ]
    public string Descricao { get; set; } = string.Empty;
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public int NumeroParcela { get; set; }
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public bool Ativo { get; set; } = true;
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public DateTime DataCriacao { get; set; }
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public DateTime DataAlteracao { get; set; }
    public int ContratoId { get; set; }
    public Contrato? Contrato { get; set; }

}
