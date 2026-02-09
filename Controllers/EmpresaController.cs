using AutoMapper;
using Contratos.Data;
using Contratos.Dto;
using Contratos.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contratos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private ContratoContext _context;
    private IMapper _mapper;

    public EmpresaController(ContratoContext contex, IMapper mapper)
    {
        _context = contex;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastroEmpresa([FromBody] EmpresaDto empresaDto)
    {
        Empresa empresa = _mapper.Map<Empresa>(empresaDto);
        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CadastroEmpresa), new { id = empresa.EmpresaId }, empresa);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaEmpresa(int id, [FromBody] EmpresaDto updateEmpresaDto)
    {
        var emp = await _context.Empresas.FirstOrDefaultAsync(empresa => empresa.EmpresaId == id);
        if (emp == null) return NotFound();
        _mapper.Map(updateEmpresaDto, emp);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RecuperaEmpresaId(int id)
    {
        var empresa = await _context.Empresas.FirstOrDefaultAsync(emp => emp.EmpresaId == id);
        if (empresa == null) return NotFound();
        var empresaDto = _mapper.Map<EmpresaDto>(empresa);
        return Ok(empresaDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    public async Task<IActionResult> RecuperaEmpresas()
    {
        var empresas =await _context.Empresas.ToListAsync();
        if (empresas == null || !empresas.Any()) return NotFound();
        var empresasDto = _mapper.Map<List<EmpresaDto>>(empresas);
        return Ok(empresasDto);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaEmpresaParcial(int id, [FromBody] JsonPatchDocument<EmpresaDto> patchDoc)
    {
        var empresa = await _context.Empresas.FirstOrDefaultAsync(emp => emp.EmpresaId == id);
        if (empresa == null) return NotFound();
        
        var empresaToPatch = _mapper.Map<EmpresaDto>(empresa);
        patchDoc.ApplyTo(empresaToPatch, ModelState);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _mapper.Map(empresaToPatch, empresa);
        await _context.SaveChangesAsync();        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletaEmpresa(int id)
    {
        var empresa = await _context.Empresas.FirstOrDefaultAsync(emp => emp.EmpresaId == id);
        if (empresa == null) return NotFound();
        
        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();        
        return NoContent();
    }

}
