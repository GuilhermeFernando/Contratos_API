using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.ContratanteDto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contratos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratanteController : ControllerBase
{
    private readonly ContratoContext _context;
    private readonly IMapper _mapper;

    public ContratanteController(ContratoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastroContratante([FromBody] CreateContratanteDto contratanteDto)
    {
        try
        {
            var contratante = _mapper.Map<Contratante>(contratanteDto);
            await _context.Contratantes.AddAsync(contratante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RecuperaContratanteId), new { id = contratante.ContratanteId }, contratante);
        }
        catch (Exception ex)
        {
            return StatusCode(500,$"Erro ao cadastrar contratante: {ex.Message}");
        }

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaContratanteId(int id)
    {
        try
        {
            var contratante = _context.Contratantes.FirstOrDefault(c => c.ContratanteId == id);
            var contratanteDto = _mapper.Map<ReadContratanteDto>(contratante);
            return Ok(contratanteDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao recuperar contratante: {ex.Message}");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RecuperaContratantes()
    {
        try
        {
            var contratantes = await _context.Contratantes.ToListAsync();
            var contratantesDto = _mapper.Map<List<ReadContratanteDto>>(contratantes);
            return Ok(contratantesDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao recuperar contratantes: {ex.Message}");
        }
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AtualizaContratante(int id, [FromBody] UpdateContratanteDto updateContratanteDto)
    {
        try 
        {
            var contratante = _context.Contratantes.FirstOrDefault(c => c.ContratanteId == id);
            _mapper.Map(updateContratanteDto, contratante);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar contratante: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaContratante(int id)
    {
        try
        {
            var contratante = _context.Contratantes.FirstOrDefault(c => c.ContratanteId == id);
            _context.Contratantes.Remove(contratante);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar contratante: {ex.Message}");
        }
    }
}
