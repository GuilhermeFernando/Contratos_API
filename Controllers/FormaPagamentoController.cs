using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.FormaPagamentoDto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;

namespace Contratos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormaPagamentoController : ControllerBase
{
    private readonly ContratoContext _context;
    private readonly IMapper _mapper;

    public FormaPagamentoController(ContratoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CadastroFormaPagamento([FromBody] CreateFormaPagamentoDto formaPagamentoDto)
    {
        var formaPagamento = _mapper.Map<FormaPagamento>(formaPagamentoDto);
        _context.FormasPagamento.Add(formaPagamento);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFormaPagamentoId), new { id = formaPagamento.FormaPagamentoId }, formaPagamento);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaFormaPagamentoId(int id)
    {
        var formaPagamento = _context.FormasPagamento.FirstOrDefault(fp => fp.FormaPagamentoId == id);
        if (formaPagamento == null) return NotFound();
        var formaPagamentoDto = _mapper.Map<ReadFormaPagamentoDto>(formaPagamento);
        return Ok(formaPagamentoDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaFormasPagamento()
    {
        var formasPagamento = _context.FormasPagamento.ToList();
        var formasPagamentoDto = _mapper.Map<List<ReadFormaPagamentoDto>>(formasPagamento);
        return Ok(formasPagamentoDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaFormaPagamento(int id, [FromBody] UpdateFormaPagamentoDto updateFormaPagamentoDto)
    {
        var formaPagamento = _context.FormasPagamento.FirstOrDefault(fp => fp.FormaPagamentoId == id);
        if (formaPagamento == null) return NotFound();
        _mapper.Map(updateFormaPagamentoDto, formaPagamento);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaFormaPagamento(int id)
    {
        var formaPagamento = _context.FormasPagamento.FirstOrDefault(fp => fp.FormaPagamentoId == id);
        if (formaPagamento == null) return NotFound();
        _context.FormasPagamento.Remove(formaPagamento);
        _context.SaveChanges();
        return NoContent();
    }

}
