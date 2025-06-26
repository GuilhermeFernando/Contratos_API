using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.ContratoDto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;



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
    public IActionResult CadastroContrato([FromBody] CreateContratoDto contratoDto)
    {
        var contrato = _mapper.Map<Contrato>(contratoDto);
        _context.Contratos.Add(contrato);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaContratoId), new { id = contrato.ContratoId }, contrato);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaContratoId(int id)
    {
        var contrato = _context.Contratos.FirstOrDefault(c => c.ContratoId == id);
        if (contrato == null) return NotFound();
        var contratoDto = _mapper.Map<ReadContratoDto>(contrato);
        return Ok(contratoDto);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaContratos()
    {
        var contratos = _context.Contratos.ToList();
        var contratosDto = _mapper.Map<List<ReadContratoDto>>(contratos);
        return Ok(contratosDto);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaContrato(int id, [FromBody] UpdateContratoDto updateContratoDto)
    {
        var contrato = _context.Contratos.FirstOrDefault(c => c.ContratoId == id);
        if (contrato == null) return NotFound();
        _mapper.Map(updateContratoDto, contrato);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaContrato(int id)
    {
        var contrato = _context.Contratos.FirstOrDefault(c => c.ContratoId == id);
        if (contrato == null) return NotFound();
        _context.Contratos.Remove(contrato);
        _context.SaveChanges();
        return NoContent();
    }
}
