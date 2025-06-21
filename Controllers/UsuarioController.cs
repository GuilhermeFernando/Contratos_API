using AutoMapper;
using Contratos.Data;
using Contratos.Data.Dto.UsuarioDto;
using Contratos.Model;
using Microsoft.AspNetCore.Mvc;

namespace Contratos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ContratoContext _context;
    private readonly IMapper _mapper;
    public UsuarioController(ContratoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CadastroUsuario([FromBody] CreateUsuarioDto usuarioDto)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaUsuarioId), new { id = usuario.UsuarioId }, usuario);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaUsuario(int id, [FromBody] UpdateUsuarioDto updateUsuarioDto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        _mapper.Map(updateUsuarioDto, usuario);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaUsuarioId(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        var usuarioDto = _mapper.Map<ReadUsuarioDto>(usuario);
        return Ok(usuarioDto);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaUsuarios()
    {
        var usuarios = _context.Usuarios.ToList();
        var usuariosDto = _mapper.Map<List<ReadUsuarioDto>>(usuarios);
        return Ok(usuariosDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaUsuario(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
        return NoContent();
    }
}
