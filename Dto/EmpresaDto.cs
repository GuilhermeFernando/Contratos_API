using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class EmpresaDto
{
    [Required(ErrorMessage = "O ID do endereço é obrigatório.")]
    public Guid EnderecoId { get; set; }
   
    [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
    public Guid UsuarioId { get; set; }
    [Required(ErrorMessage = "A razão social é obrigatória.")]
    public string RazaoSocial { get; set; } = string.Empty;
    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    public string CNPJ { get; set; } = string.Empty;
    [Required(ErrorMessage = "O nome fantasia é obrigatório.")]
    public string NomeFantasia { get; set; } = string.Empty;
    [Required(ErrorMessage = "A inscrição estadual é obrigatória.")]
    public string IE { get; set; } = string.Empty;
    public string IM { get; set; } = string.Empty;
    public string NaturezaJuridica { get; set; } = string.Empty;    
    public DateTime DataAbertura { get; set; }
}
