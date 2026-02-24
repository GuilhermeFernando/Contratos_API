using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto;

public class TokenDto
{
    [Required(ErrorMessage = "O token é obrigatório.")]
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}
