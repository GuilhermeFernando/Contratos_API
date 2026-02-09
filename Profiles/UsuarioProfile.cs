using AutoMapper;
using Contratos.Dto;
using Contratos.Model;

namespace Contratos.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<UsuarioDto,Usuario>().ForMember(desc => desc.Senha, opts => opts.Ignore());

        CreateMap<Usuario, UsuarioResponseDto>().ReverseMap();
    }
}
