using AutoMapper;
using Contratos.Data.Dto.ContratanteDto;
using Contratos.Model;

namespace Contratos.Profiles;

public class ContratanteProfile : Profile
{
    public ContratanteProfile()
    {
        CreateMap<Contratante, CreateContratanteDto>().ReverseMap()
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
        CreateMap<UpdateContratanteDto, Contratante>()
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
        CreateMap<Contratante, ReadContratanteDto>()
            .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
    }
}
