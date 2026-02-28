namespace Contratos.Model
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid  UsuarioId { get; set; }
        public string  Token { get; set; } = string.Empty;
        public DateTime Expirantion { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Usuario? Usuario { get; set; }

    }
}
