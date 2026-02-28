using System.ComponentModel.DataAnnotations;

namespace Contratos.Dto
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
