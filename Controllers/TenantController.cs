namespace Contratos.Controllers;

using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.EmpresaDto;
using Contratos.Data.Dto.TenantDto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TenantController : ControllerBase
{
    private ContratoContext _context;
    private IMapper _mapper;

    public TenantController(ContratoContext context, IMapper mapper)
    {
        mapper = _mapper;
        context = _context;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CadastroTenant([FromBody] CreateTenantDto tenantDto)
    {
        var tenant = _mapper.Map<Tenant>(tenantDto);
        _context.Tenants.Add(tenant);
        _context.SaveChanges();

        return CreatedAtAction(nameof(CadastroTenant), new { id = tenant.TenantId }, tenant);
    }

    [HttpPut("[id]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaTenant(int id, [FromBody] UpdateTenantDto updateTenantDto)
    {
        var tnt = _context.Tenants.FirstOrDefault(tnts => tnts.TenantId == id);
        if (tnt == null) return NotFound();
        _mapper.Map(updateTenantDto, tnt);
        _context.SaveChanges();
        return NoContent();
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaTenantId(int id)
    {
        var tenant = _context.Tenants.FirstOrDefault(tnts => tnts.TenantId == id);
        if (tenant == null) return NotFound();
        var tenantDto = _mapper.Map<ReadTenantDto>(tenant);
        return Ok(tenantDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IEnumerable<ReadTenantDto> RecuperaTenants()
    {
        return _mapper.Map<IEnumerable<ReadTenantDto>>(_context.Tenants.ToList());
    }
}
