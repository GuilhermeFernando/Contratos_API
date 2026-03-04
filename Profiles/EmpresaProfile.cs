using Contratos.Model;
using AutoMapper;
using Contratos.Dto;

namespace Contratos.Profiles;

public class EmpresaProfile : Profile
{
    public EmpresaProfile()
    {
        CreateMap<EmpresaDto, Empresa>()
            .ForMember(dest => dest.Endereco, opts => opts.Ignore())
            .ForMember(dest => dest.Usuario, opts => opts.Ignore());

        CreateMap<Empresa, EmpresaDto>()
            .ForMember(dest => dest.EnderecoId, opts => opts.MapFrom(src => src.EnderecoId))
            .ForMember(dest => dest.UsuarioId, opts => opts.MapFrom(src => src.UsuarioId));
    }
}
