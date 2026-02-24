using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class EnderecoDto
{
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public Guid EnderecoId { get; set; }
    public string CEP { get; set; } = string.Empty;
    [Required(ErrorMessage = "O logradouro é obrigatório.")]
    public string Logradouro { get; set; } = string.Empty;
    [Required(ErrorMessage = "O número é obrigatório.")]
    public string Numero { get; set; } = string.Empty;
    [Required(ErrorMessage = "O bairro é obrigatório.")]
    public string Bairro { get; set; } = string.Empty;
    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string Cidade { get; set; } = string.Empty;
    [Required(ErrorMessage = "O estado é obrigatório.")]
    public string Estado { get; set; } = string.Empty;
    [Required(ErrorMessage = "O país é obrigatório.")]
    public string Pais { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;
    public Guid EmpresaId { get; set; }
}
