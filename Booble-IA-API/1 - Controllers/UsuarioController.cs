using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

                // Return both legacy token and OAuth2 information
                var response = new
                {
                    Authtoken = token,
                    access_token = token,
                    token_type = "Bearer",
                    expires_in = 3600,
                    // OAuth2 endpoints for clients that want to use them
                    oauth2_endpoints = new
                    {
                        authorization_endpoint = $"{Request.Scheme}://{Request.Host}/connect/authorize",
                        token_endpoint = $"{Request.Scheme}://{Request.Host}/connect/token",
                        userinfo_endpoint = $"{Request.Scheme}://{Request.Host}/api/oauth/userinfo",
                        revocation_endpoint = $"{Request.Scheme}://{Request.Host}/connect/revocation"
                    }
                };

                return Ok(response);
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

        [HttpGet]
        [Route("PerfilUsuario")]
        [Authorize] // Now requires authentication
        public async Task<IActionResult> PerfilUsuario()
        {
            try
            {
                // Get user ID from JWT token claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { message = "Token inválido ou usuário não encontrado." });
                }

                var userProfile = await _usuarioService.Get(userId);
                
                if (userProfile == null)
                {
                    return NotFound(new { message = "Perfil do usuário não encontrado." });
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno ao buscar perfil do usuário: {ex.Message}" });
            }
        }

        // Alternative endpoint that accepts user ID for backward compatibility
        [HttpGet]
        [Route("PerfilUsuario/{idUsuario}")]
        [Authorize]
        public async Task<IActionResult> PerfilUsuarioPorId(int idUsuario)
        {
            try
            {
                // Verify that the requesting user can access this profile
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var requestingUserId))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                // For now, users can only access their own profile
                // You can modify this logic to allow friends to see each other's profiles
                if (requestingUserId != idUsuario)
                {
                    return Forbid("Você só pode acessar seu próprio perfil.");
                }

                return Ok(await _usuarioService.Get(idUsuario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno ao buscar perfil do usuário: {ex.Message}" });
            }
        }
    }
}
