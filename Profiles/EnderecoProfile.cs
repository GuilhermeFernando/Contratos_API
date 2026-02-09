using AutoMapper;
using Contratos.Dto;
using Contratos.Model;

namespace Contratos.Profiles;

public class EnderecoProfile :Profile
{
    public EnderecoProfile()
    {
        CreateMap<Endereco, EnderecoDto>().ReverseMap();
         
    }
}
