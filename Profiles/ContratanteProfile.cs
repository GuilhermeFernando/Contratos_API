using AutoMapper;
using Contratos.Dto;
using Contratos.Model;

namespace Contratos.Profiles;

public class ContratanteProfile : Profile
{
    public ContratanteProfile()
    {
        CreateMap<Contratante, ContratanteDto>().ReverseMap()
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
       
    }
}
