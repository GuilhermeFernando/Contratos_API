using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.EmpresaDto;
using Contratos.Model;
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
        Endereco endereco = _mapper.Map<Endereco>(empresaDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(CadastroEmpresa), new { id = endereco.EnderecoId }, endereco);
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


}
