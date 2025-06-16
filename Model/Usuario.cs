using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Usuario
{
    [Key]
    [Required]
    public int UsuarioId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "O nome de usuário deve ter no máximo 100 caracteres.")]
    public string NomeUsuario { get; set; }
    public ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();

}
