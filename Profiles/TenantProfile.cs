using AutoMapper;
using Contratos.Dto;
using Contratos.Model;

namespace Contratos.Profiles;

public class TenantProfile : Profile
{
    public TenantProfile()
    {
        CreateMap<Tenant, TenantDto>().ReverseMap();
    }
}
