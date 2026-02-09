namespace Contratos.Controllers;

using AutoMapper;
using Contratos.Data;
using Contratos.Dto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TenantController : ControllerBase
{
    private ContratoContext _context;
    private IMapper _mapper;

    public TenantController(ContratoContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastroTenant([FromBody] TenantDto tenantDto)
    {
        Tenant tenant = _mapper.Map<Tenant>(tenantDto);
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(RecuperaTenantId), new { id = tenant.TenantId }, tenant);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaTenant(int id, [FromBody] TenantDto updateTenantDto)
    {
        var tnt = await _context.Tenants.FirstOrDefaultAsync(tnts => tnts.TenantId == id);
        if (tnt == null) return NotFound();
        _mapper.Map(updateTenantDto, tnt);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RecuperaTenantId(int id)
    {
        var tenant = await _context.Tenants.FirstOrDefaultAsync(tnts => tnts.TenantId == id);
        if (tenant == null) return NotFound();
        var tenantDto = _mapper.Map<TenantDto>(tenant);
        return Ok(tenantDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<IEnumerable<TenantDto>> RecuperaTenants()
    {
        return _mapper.Map<IEnumerable<TenantDto>>(await _context.Tenants.ToListAsync());
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletaTenant(int id)
    {
        var tenant = await _context.Tenants.FirstOrDefaultAsync(tnts => tnts.TenantId == id);
        if (tenant == null) return NotFound();
        _context.Tenants.Remove(tenant);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
