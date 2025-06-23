using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.EnderecoDto;
using Contratos.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contratos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnderecoController : ControllerBase
{
    private ContratoContext _context;
    private IMapper _mapper;

    public EnderecoController(IMapper mapper,ContratoContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CadastroEndereco([FromBody] CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();        
        return CreatedAtAction(nameof(CadastroEndereco), new { id = endereco.EnderecoId }, endereco);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaEnderecoId(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(end => end.EnderecoId == id);
        if (endereco == null) return NotFound();
        var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
        return Ok(enderecoDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaEnderecos()
    {
        var enderecos = _context.Enderecos.ToList();
        if (enderecos == null || !enderecos.Any()) return NotFound();
        var enderecosDto = _mapper.Map<List<ReadEnderecoDto>>(enderecos);
        return Ok(enderecosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto updateEnderecoDto)
    {
        var end = _context.Enderecos.FirstOrDefault(endereco => endereco.EnderecoId == id);
        if (end == null) return NotFound();
        _mapper.Map(updateEnderecoDto, end);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaEndereco(int id)
    {
        var end = _context.Enderecos.FirstOrDefault(endereco => endereco.EnderecoId == id);
        if (end == null) return NotFound();
        _context.Enderecos.Remove(end);
        _context.SaveChanges();
        return NoContent();
    }
}
