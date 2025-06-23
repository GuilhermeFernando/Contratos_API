using AutoMapper;
using Contratos.Data.Dto.EnderecoDto;
using Contratos.Model;

namespace Contratos.Profiles;

public class EnderecoProfile :Profile
{
    public EnderecoProfile()
    {
        CreateMap<Endereco, CreateEnderecoDto>().ReverseMap();
        CreateMap<UpdateEnderecoDto, Endereco>().ReverseMap();
        CreateMap<Endereco, ReadEnderecoDto>();        
    }
}
