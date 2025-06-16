using AutoMapper;
using Contratos.Data.Dto;
using Contratos.Model;

namespace Contratos.Profiles;

public class EmpresaProfile : Profile
{
    public EmpresaProfile()
    {
        CreateMap<CreateEmpresaDto, Empresa>();
        CreateMap<UpdateEmpresaDto, Empresa>();
        CreateMap<Empresa, UpdateEmpresaDto>();
        CreateMap<Empresa, ReadEmpresaDto>().ForMember(enderecoDto => enderecoDto.Endereco, opt => opt.MapFrom(endereco => endereco.Endereco)); ;

    }
}
