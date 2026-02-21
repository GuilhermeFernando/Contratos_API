using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Usuario
{
    [Key]
    [Required]
    public Guid UsuarioId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "O nome de usuário deve ter no máximo 100 caracteres.")]
    public string NomeUsuario { get; set; } 
    [Required]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string UrlLogo { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }

}
