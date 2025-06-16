using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Endereco
{
    [Key]
    [Required]
    public int EnderecoId { get; set; }
    [Required]
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
    public virtual Empresa Empresa { get; set; }
}
