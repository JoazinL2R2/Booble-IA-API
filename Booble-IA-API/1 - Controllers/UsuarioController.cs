using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

            
            
            //criar regra de negocio para verificar se o usuário está ativo ou não, etc.
            //criar interacao com o banco de dados (repository)
            //retornar o usuário logado com os dados do perfil, como nome, email, telefone, etc.


        }
        //criar endpoint para retornar dados do usuario ( perfil) GET: usuarios/PerfilUsuario
        [HttpGet]
        [Route("PerfilUsuario")]
        public async Task<IActionResult> PerfilUsuario(int idUsuario) 
        {
            try
            {
                return Ok(await _usuarioService.Get(idUsuario));
            }
            catch  (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno ao buscar perfil do usuário: {ex.Message}" });
            }


        }
    }
}
