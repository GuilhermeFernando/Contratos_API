using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Empresa
{
    [Key]
    [Required]
    public int EmpresaId { get; set; }
    [Required]
    public string RazaoSocial { get; set; }
    [Required]
    public string CNPJ { get; set; }
    public string NomeFantasia { get; set; }
    public string IE { get; set; }
    public string IM { get; set; }
    public string NaturezaJuridica { get; set; }
    public DateTime DataAbertura { get; set; }
    public int EnderecoId { get; set; }
    [Required]
    public Endereco Endereco { get; set; }
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
