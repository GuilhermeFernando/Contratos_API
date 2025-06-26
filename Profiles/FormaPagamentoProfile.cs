
using AutoMapper;
using Contratos.Model;
using Contratos.Data.Dto.FormaPagamentoDto;

namespace Contratos.Profiles;

public class FormaPagamentoProfile : Profile
{
    public FormaPagamentoProfile()
    {
        CreateMap<FormaPagamento,ReadFormaPagamentoDto>().ReverseMap();
        CreateMap<FormaPagamento,CreateFormaPagamentoDto>().ReverseMap();
        CreateMap<FormaPagamento,UpdateFormaPagamentoDto>().ReverseMap();
    }
}
