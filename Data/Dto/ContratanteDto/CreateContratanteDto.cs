using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Data.Dto.ContratanteDto;

public class CreateContratanteDto
{
    [Required]
    public int EmpresaId { get; set; }
    [Required]
    public Empresa Empresa { get; set; }
    [Required]
    public string RazaoSocial { get; set; }
    [Required]
    public int EnderecoId { get; set; }
    [Required]
    public Endereco Endereco { get; set; }
    [Required]
    public string NomeFantasia { get; set; }
    [Required]
    public string Documento { get; set; }
}
