using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class LoginDto
{
    [Required(ErrorMessage = "Nome de usuário obrigatório.")]
    public string NomeUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha obrigatória.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;
}