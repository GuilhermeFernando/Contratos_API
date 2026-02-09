
using AutoMapper;
using Contratos.Model;
using Contratos.Dto;

namespace Contratos.Profiles;

public class FormaPagamentoProfile : Profile
{
    public FormaPagamentoProfile()
    {
        CreateMap<FormaPagamento,FormaPagamentoDto>().ReverseMap();
    }
}
