using System.Runtime.CompilerServices;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booble_IA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("Cadastro")]
        public async Task<IActionResult> Cadastro([FromBody] UsuarioDTO cadastroRequest)
        {
            try
            {
                var cadastrado = await _usuarioService.Cadastro(cadastroRequest);

                if (cadastrado == null)
                    return BadRequest(new { message = "Não foi possível realizar o cadastro do usuário." });

                return Ok(cadastrado);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno ao cadastrar usuário: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO loginRequest)
        {
            try
            {
                var token = await _usuarioService.Login(loginRequest);

                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { message = "Email ou senha inválidos." });

                return Ok(new { Authtoken = token });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno ao realizar login: {ex.Message}" });
            }
        }
    }
}
