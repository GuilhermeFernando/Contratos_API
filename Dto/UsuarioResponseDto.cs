using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class UsuarioResponseDto
{
    [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome de usuário deve ter no máximo 100 caracteres.")]
    public string NomeUsuario { get; set; } = string.Empty;
    [Required(ErrorMessage = "O email é obrigatório.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;
    public string UrlLogo { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
  
}
