using AutoMapper;
using Contratos.Data;
using Contratos.Dto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult> CadastroFormaPagamento([FromBody] FormaPagamentoDto formaPagamentoDto)
    {
        var formaPagamento = _mapper.Map<FormaPagamento>(formaPagamentoDto);
        _context.FormasPagamento.Add(formaPagamento);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(RecuperaFormaPagamentoId), new { id = formaPagamento.FormaPagamentoId }, formaPagamento);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RecuperaFormaPagamentoId(int id)
    {
        var formaPagamento = await _context.FormasPagamento.FirstOrDefaultAsync(fp => fp.FormaPagamentoId == id);
        if (formaPagamento == null) return NotFound();
        var formaPagamentoDto = _mapper.Map<FormaPagamentoDto>(formaPagamento);
        return Ok(formaPagamentoDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RecuperaFormasPagamento()
    {
        var formasPagamento = await _context.FormasPagamento.ToListAsync();
        var formasPagamentoDto = _mapper.Map<List<FormaPagamentoDto>>(formasPagamento);
        return Ok(formasPagamentoDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaFormaPagamento(int id, [FromBody] FormaPagamentoDto updateFormaPagamentoDto)
    {
        var formaPagamento = await _context.FormasPagamento.FirstOrDefaultAsync(fp => fp.FormaPagamentoId == id);
        if (formaPagamento == null) return NotFound();
        _mapper.Map(updateFormaPagamentoDto, formaPagamento);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletaFormaPagamento(int id)
    {
        var formaPagamento = await _context.FormasPagamento.FirstOrDefaultAsync(fp => fp.FormaPagamentoId == id);
        if (formaPagamento == null) return NotFound();
        _context.FormasPagamento.Remove(formaPagamento);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}
