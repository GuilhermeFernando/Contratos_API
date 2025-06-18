using Contratos.Model;
using System.ComponentModel.DataAnnotations;

namespace Contratos.Data.Dto.UsuarioDto;

public class UpdateUsuarioDto
{
    [StringLength(100, ErrorMessage = "O nome de usuário deve ter no máximo 100 caracteres.")]
    public string NomeUsuario { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string UrlLogo { get; set; }
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
