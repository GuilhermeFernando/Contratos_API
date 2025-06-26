using AutoMapper;
using Contratos.Data.Dto.ContratoDto;
using Contratos.Model;

namespace Contratos.Profiles;

public class ContratoProfile : Profile
{
    public ContratoProfile()
    {
        CreateMap<Contrato,ReadContratoDto>().ReverseMap();
        CreateMap<Contrato,CreateContratoDto>().ReverseMap();
        CreateMap<Contrato,UpdateContratoDto>().ReverseMap();
    }
}
