using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Empresa
{
    [Key]   
    public required Guid EmpresaId { get; set; }
    [Required]
    public required Guid EnderecoId { get; set; } 
    public required Endereco Endereco { get; set; }   
    public required Guid UsuarioId { get; set; }   
    public required Usuario Usuario { get; set; }
    public required string RazaoSocial { get; set; }    
    public required string CNPJ { get; set; }
    public required string NomeFantasia { get; set; }
    public required string IE { get; set; }
    public required string IM { get; set; }
    public required string NaturezaJuridica { get; set; }
    public required DateTime DataAbertura { get; set; }
     
}
