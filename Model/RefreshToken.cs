// using Contratos.Interface;

namespace Contratos.Model
{
public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid  UsuarioId { get; set; }
        public string  Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; } = false;
        // TenantId removed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Usuario? Usuario { get; set; }

    }
}
