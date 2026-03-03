using AutoMapper;
using Contratos.Dto;
using Contratos.Model;

namespace Contratos.Profiles;

public class ContratanteProfile : Profile
{
    public ContratanteProfile()
    {
        CreateMap<ContratanteDto, Contratante>()
            .ForMember(dest => dest.ContratanteId, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Empresa, opt => opt.Ignore())
            .ForMember(dest => dest.Endereco, opt => opt.Ignore())
            .ForMember(dest => dest.Tenant, opt => opt.Ignore());

        CreateMap<Contratante, ContratanteDto>();
    }
}
