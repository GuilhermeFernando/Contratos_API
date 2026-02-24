using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Endereco
{
    [Key]
    public required Guid EnderecoId { get; set; }
    public required string CEP { get; set; }
    public required string Logradouro { get; set; }
    public required string Numero { get; set; }   
    public required string Bairro { get; set; }
    public required string Cidade { get; set; } 
    public required string Estado { get; set; }
    public required string Pais { get; set; }
    public required string Complemento { get; set; }
    
}
