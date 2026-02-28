using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Contratante
{
    [Key] 
    public required Guid ContratanteId { get; set; }   
    public required Guid EmpresaId { get; set; }  
    public required Empresa Empresa { get; set; }
    public required string RazaoSocial { get; set; }
    public required Guid EnderecoId { get; set; }
    public required Endereco Endereco { get; set; }
    public required string NomeFantasia { get; set; }   
    public required string Documento { get; set; }

}
