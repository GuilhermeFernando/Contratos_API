using AutoMapper;
using Contratos.Dto;
using Contratos.Model;

namespace Contratos.Profiles;

public class ContratoProfile : Profile
{
    public ContratoProfile()
    {        
        CreateMap<Contrato,ContratoDto>().ReverseMap();
    }
}
