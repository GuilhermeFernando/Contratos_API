using System.ComponentModel.DataAnnotations;

namespace Contratos.Data.Dto;

public class UpdateEmpresaDto
{
    public string RazaoSocial { get; set; }   
    public string CNPJ { get; set; }
    public string NomeFantasia { get; set; }
    public string IE { get; set; }
    public string IM { get; set; }
    public string NaturezaJuridica { get; set; }
    public DateTime DataAbertura { get; set; }
}
