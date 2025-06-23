using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.EmpresaDto;
using Contratos.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult CadastroEmpresa([FromBody] CreateEmpresaDto empresaDto)
    {
        Empresa empresa = _mapper.Map<Empresa>(empresaDto);
        _context.Empresas.Add(empresa);
        _context.SaveChanges();
        return CreatedAtAction(nameof(CadastroEmpresa), new { id = empresa.EmpresaId }, empresa);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaEmpresa(int id, [FromBody] UpdateEmpresaDto updateEmpresaDto)
    {
        var emp = _context.Empresas.FirstOrDefault(empresa => empresa.EmpresaId == id);
        if (emp == null) return NotFound();
        _mapper.Map(updateEmpresaDto, emp);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaEmpresaId(int id)
    {
        var empresa = _context.Empresas.FirstOrDefault(emp => emp.EmpresaId == id);
        if (empresa == null) return NotFound();
        var empresaDto = _mapper.Map<ReadEmpresaDto>(empresa);
        return Ok(empresaDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    public IActionResult RecuperaEmpresas()
    {
        var empresas = _context.Empresas.ToList();
        if (empresas == null || !empresas.Any()) return NotFound();
        var empresasDto = _mapper.Map<List<ReadEmpresaDto>>(empresas);
        return Ok(empresasDto);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaEmpresaParcial(int id, [FromBody] JsonPatchDocument<UpdateEmpresaDto> patchDoc)
    {
        var empresa = _context.Empresas.FirstOrDefault(emp => emp.EmpresaId == id);
        if (empresa == null) return NotFound();
        
        var empresaToPatch = _mapper.Map<UpdateEmpresaDto>(empresa);
        patchDoc.ApplyTo(empresaToPatch, ModelState);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _mapper.Map(empresaToPatch, empresa);
        _context.SaveChanges();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaEmpresa(int id)
    {
        var empresa = _context.Empresas.FirstOrDefault(emp => emp.EmpresaId == id);
        if (empresa == null) return NotFound();
        
        _context.Empresas.Remove(empresa);
        _context.SaveChanges();
        
        return NoContent();
    }

}
