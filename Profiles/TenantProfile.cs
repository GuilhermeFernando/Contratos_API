using AutoMapper;
using Contratos.Data.Dto.TenantDto;
using Contratos.Model;

namespace Contratos.Profiles;

public class TenantProfile : Profile
{
    public TenantProfile()
    {
        CreateMap<Tenant, CreateTenantDto>();
        CreateMap<Tenant, UpdateTenantDto>();
        CreateMap<UpdateTenantDto,Tenant>();
        CreateMap<CreateTenantDto, Tenant>();
    }
}
