using AutoMapper;
using Contratos.Data;
using Contratos.Dto;
using Contratos.Interface;
using Contratos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contratos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastroUsuario([FromBody] UsuarioDto usuarioDto)
    {

        var usuarioExiste = _context.Usuario.Any(u => u.NomeUsuario == usuarioDto.NomeUsuario);
        if (usuarioExiste)
            return BadRequest("Nome de usuário já cadastrado.");

        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario.Senha = _passwordHasher.HashPassword(usuario, usuarioDto.Senha);

        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(RecuperaUsuarioId), new { id = usuario.UsuarioId }, new {usuario.UsuarioId, usuario.NomeUsuario});
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if(loginDto == null || string.IsNullOrWhiteSpace(loginDto.NomeUsuario) || string.IsNullOrWhiteSpace(loginDto.Senha))
            return Unauthorized("Nome de usuário e senha são obrigatórios.");

        var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.NomeUsuario == loginDto.NomeUsuario);
        if (usuario == null)
            return Unauthorized("Credenciais inválidas.");

        var resultadoVerificacao = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, loginDto.Senha);
        if (resultadoVerificacao == PasswordVerificationResult.Failed)
            return Unauthorized("Credenciais inválidas.");
        
        var (acessToken, acessTokenExpiration) = _jwtService.GenerateToken(usuario);

        var(refreshToken, refreshTokenExpiration) = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
           UsuarioId = usuario.UsuarioId,
           Token = refreshToken,
           Expiration = refreshTokenExpiration
        };

        _context.RefreshToken.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

        return Ok(new TokenResponseDto
        {
            AccessToken = acessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiration = acessTokenExpiration,
            RefreshTokenExpiration = refreshTokenExpiration

        });
       
    }



    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizaUsuario(Guid id, [FromBody] UsuarioDto updateUsuarioDto)
    {
        var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        usuario.NomeUsuario = updateUsuarioDto.NomeUsuario;
        usuario.Email = updateUsuarioDto.Email;
        usuario.Telefone = updateUsuarioDto.Telefone;

        if (!string.IsNullOrWhiteSpace(updateUsuarioDto.Senha))
        {
            usuario.Senha = _passwordHasher.HashPassword(usuario, updateUsuarioDto.Senha);
        }


        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RecuperaUsuarioId(Guid id)
    {
        var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        var usuarioDto = _mapper.Map<UsuarioResponseDto>(usuario);
        return Ok(usuarioDto);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RecuperaUsuarios()
    {
        var usuarios = await _context.Usuario.ToListAsync();
        var usuariosDto = _mapper.Map<List<UsuarioResponseDto>>(usuarios);
        return Ok(usuariosDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletaUsuario(Guid id)
    {
        var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.UsuarioId == id);
        if (usuario == null) return NotFound();
        _context.Usuario.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return Unauthorized("Refresh token é obrigatório.");

        try
        {
            var storedRefreshToken = await _context.RefreshToken
                .IgnoreQueryFilters()
                .Include(rt => rt.Usuario)
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

            if (storedRefreshToken == null || storedRefreshToken.IsRevoked)
                return Unauthorized("Refresh token inválido ou revogado.");

            if (storedRefreshToken.Expiration < DateTime.UtcNow)
                return Unauthorized("Refresh token expirado.");

            var (newAccessToken, newAccessTokenExpiration) = 
                _jwtService.GenerateAccessToken(storedRefreshToken.Usuario!);

            var (newRefreshToken, newRefreshTokenExpiration) = 
                _jwtService.GenerateRefreshToken();

            storedRefreshToken.IsRevoked = true;

            var newRefreshTokenEntity = new RefreshToken
            {
                UsuarioId = storedRefreshToken.UsuarioId,
                Token = newRefreshToken,
                Expiration = newRefreshTokenExpiration
            };

            _context.RefreshToken.Add(newRefreshTokenEntity);
            await _context.SaveChangesAsync();

            return Ok(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiration = newAccessTokenExpiration,
                RefreshTokenExpiration = newRefreshTokenExpiration
            });
        }
        catch
        {
            return Unauthorized("Erro ao processar refresh token.");
        }
    }
}
