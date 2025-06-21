using AutoMapper;
using Contratos.Data.Dto.TenantDto;
using Contratos.Data.Dto.UsuarioDto;
using Contratos.Model;

namespace Contratos.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, CreateUsuarioDto>();
        CreateMap<Usuario, UpdateUsuarioDto>();
        CreateMap<CreateUsuarioDto, Usuario>();
        CreateMap<UpdateUsuarioDto, Usuario>();
        CreateMap<Usuario, ReadUsuarioDto>();


    }
}
