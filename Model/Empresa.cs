using System.ComponentModel.DataAnnotations;

namespace Contratos.Model;

public class Empresa
{
    [Key]
    [Required]
    public Guid EmpresaId { get; set; }
    [Required]
    public Guid EnderecoId { get; set; } 
    public Endereco Endereco { get; set; }   
    public Guid UsuarioId { get; set; }   
    public Usuario Usuario { get; set; }
    [Required]
    public Guid TenantId { get; set; }
    [Required]
    public Tenant Tenant { get; set; }
    [Required]
    public string RazaoSocial { get; set; }
    [Required]
    public string CNPJ { get; set; }
    public string NomeFantasia { get; set; }
    public string IE { get; set; }
    public string IM { get; set; }
    public string NaturezaJuridica { get; set; }
    public DateTime DataAbertura { get; set; }
     
}
