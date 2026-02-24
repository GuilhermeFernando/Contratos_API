using AutoMapper;
using Contratos.Data;
using Contratos.Dto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Contratos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratoController : ControllerBase
{
    private readonly ContratoContext _context;
    private readonly IMapper _mapper;
    public ContratoController(ContratoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastroContrato([FromBody] ContratoDto contratoDto)
    {
        var contrato = _mapper.Map<Contrato>(contratoDto);
        _context.Contrato.Add(contrato);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(RecuperaContratoId), new { id = contrato.ContratoId }, contrato);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RecuperaContratoId(Guid id)
    {
        var contrato = await _context.Contrato.FirstOrDefaultAsync(c => c.ContratoId == id);
        if (contrato == null) return NotFound();
        var contratoDto = _mapper.Map<ContratoDto>(contrato);
        return Ok(contratoDto);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult >RecuperaContratos()
    {
        var contratos = await _context.Contrato.ToListAsync();
        var contratosDto = _mapper.Map<List<ContratoDto>>(contratos);
        return Ok(contratosDto);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaContrato(Guid id, [FromBody] ContratoDto updateContratoDto)
    {
        var contrato = await _context.Contrato.FirstOrDefaultAsync(c => c.ContratoId == id);
        if (contrato == null) return NotFound();
        _mapper.Map(updateContratoDto, contrato);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletaContrato(Guid id)
    {
        var contrato = await _context.Contrato.FirstOrDefaultAsync(c => c.ContratoId == id);
        if (contrato == null) return NotFound();
        _context.Contrato.Remove(contrato);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
