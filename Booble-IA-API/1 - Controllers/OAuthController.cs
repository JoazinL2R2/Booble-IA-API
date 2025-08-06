using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Models;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API.DTO;
using System.Security.Claims;

namespace Booble_IA_API._1___Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<OAuthController> _logger;

        public OAuthController(
            IUsuarioService usuarioService, 
            ITokenService tokenService,
            ILogger<OAuthController> logger)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromForm] TokenRequest request)
        {
            try
            {
                if (request.grant_type == "password")
                {
                    return await HandlePasswordGrant(request);
                }
                else if (request.grant_type == "refresh_token")
                {
                    return await HandleRefreshTokenGrant(request);
                }

                return BadRequest(new { error = "unsupported_grant_type" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OAuth token endpoint");
                return StatusCode(500, new { error = "server_error", error_description = ex.Message });
            }
        }

        private async Task<IActionResult> HandlePasswordGrant(TokenRequest request)
        {
            var loginDto = new UsuarioDTO
            {
                Des_Email = request.username,
                Senha = request.password
            };

            var token = await _usuarioService.Login(loginDto);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { error = "invalid_grant", error_description = "Invalid username or password" });
            }

            // Create additional OAuth2 response
            var tokenResponse = new
            {
                access_token = token,
                token_type = "Bearer",
                expires_in = 3600,
                refresh_token = Guid.NewGuid().ToString(), // In production, implement proper refresh token logic
                scope = request.scope ?? "booble_api"
            };

            return Ok(tokenResponse);
        }

        private async Task<IActionResult> HandleRefreshTokenGrant(TokenRequest request)
        {
            // In a production environment, validate the refresh token against your database
            // For now, this is a simplified implementation
            
            if (string.IsNullOrEmpty(request.refresh_token))
            {
                return BadRequest(new { error = "invalid_request", error_description = "refresh_token is required" });
            }

            // Here you would validate the refresh token and get user info
            // For now, returning an error - implement proper refresh token logic
            return BadRequest(new { error = "invalid_grant", error_description = "Invalid refresh token" });
        }

        [HttpGet("userinfo")]
        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized();
                }

                var user = await _usuarioService.Get(userId);
                
                if (user == null)
                {
                    return NotFound(new { error = "user_not_found" });
                }

                var userInfo = new
                {
                    sub = user.Idf_Usuario.ToString(),
                    name = user.Des_Nme,
                    email = user.Des_Email,
                    phone = user.Num_Telefone,
                    gender = user.Flg_Sexo?.ToString() ?? "",
                    birthdate = user.Dta_Nascimento.ToString("yyyy-MM-dd"),
                    created_at = user.Dta_Cadastro?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    updated_at = user.Dta_Alteracao?.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user info");
                return StatusCode(500, new { error = "server_error" });
            }
        }

        [HttpPost("revoke")]
        [Authorize]
        public IActionResult RevokeToken([FromForm] RevokeTokenRequest request)
        {
            // In production, implement proper token revocation
            // For now, just return success
            return Ok();
        }
    }

    public class TokenRequest
    {
        public string? grant_type { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? refresh_token { get; set; }
        public string? scope { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }
    }

    public class RevokeTokenRequest
    {
        public string? token { get; set; }
        public string? token_type_hint { get; set; }
    }
}