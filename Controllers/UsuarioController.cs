using AutoMapper;
using Contratos.Data;
using Contratos.Dto;
using Contratos.Interface;
using Contratos.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contratos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ContratoContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IJwtService _jwtService;
    public UsuarioController(ContratoContext context, IMapper mapper, IPasswordHasher<Usuario> passwordHasher, IJwtService jwtService)
    {
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }


    [HttpPost("registrar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastroUsuario([FromBody] UsuarioDto usuarioDto)
    {
        if (usuarioDto == null || string.IsNullOrWhiteSpace(usuarioDto.Email)) 
            return BadRequest("Email obrigatório.");

        var usuarioExiste = _context.Usuarios.Any(u => u.Email == usuarioDto.Email);
        if(usuarioExiste)
            return BadRequest("Email ja cadastrado.");

        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario.Senha = _passwordHasher.HashPassword(usuario, usuarioDto.Senha);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(RecuperaUsuarioId), new { id = usuario.UsuarioId }, null);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] UsuarioDto loginDto)
    {
        if(loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Senha))
            return Unauthorized("Email e senha são obrigatorio.");

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (usuario == null)
            return Unauthorized("Credenciais inválidas.");

        var resultadoVerificacao = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, loginDto.Senha);
        if (resultadoVerificacao == PasswordVerificationResult.Failed)
            return Unauthorized("Credenciais inválidas.");

        var (token, expiration) = _jwtService.GenerateToken(usuario);
        return Ok(new TokenDto { Token = token, Expiration = expiration });
    }



    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaUsuario(int id, [FromBody] UsuarioDto updateUsuarioDto)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        _mapper.Map(updateUsuarioDto, usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RecuperaUsuarioId(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        var usuarioDto = _mapper.Map<UsuarioResponseDto>(usuario);
        return Ok(usuarioDto);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RecuperaUsuarios()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        var usuariosDto = _mapper.Map<List<UsuarioResponseDto>>(usuarios);
        return Ok(usuariosDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletaUsuario(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
