using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class EnderecoDto
{
    public string CEP { get; set; }
    [Required]
    public string Logradouro { get; set; }
    [Required]
    public string Numero { get; set; }
    [Required]
    public string Bairro { get; set; }
    [Required]
    public string Cidade { get; set; }
    [Required]
    public string Estado { get; set; }
    [Required]
    public string Pais { get; set; }
    public string Complemento { get; set; }
    public int EmpresaId { get; set; }
}
