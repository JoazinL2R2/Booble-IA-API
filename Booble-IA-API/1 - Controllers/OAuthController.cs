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
        [AllowAnonymous] // Public endpoint for token generation
        public async Task<IActionResult> Token([FromForm] TokenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.grant_type))
                {
                    return BadRequest(new { error = "invalid_request", error_description = "grant_type is required" });
                }

                if (request.grant_type == "password")
                {
                    return await HandlePasswordGrant(request);
                }
                else if (request.grant_type == "refresh_token")
                {
                    return await HandleRefreshTokenGrant(request);
                }

                return BadRequest(new { error = "unsupported_grant_type", error_description = $"Grant type '{request.grant_type}' is not supported" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OAuth token endpoint");
                return StatusCode(500, new { error = "server_error", error_description = "An internal server error occurred" });
            }
        }

        private async Task<IActionResult> HandlePasswordGrant(TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest(new { error = "invalid_request", error_description = "Username and password are required" });
            }

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

            // Create OAuth2 compliant response
            var tokenResponse = new
            {
                access_token = token,
                token_type = "Bearer",
                expires_in = 3600,
                refresh_token = Guid.NewGuid().ToString(), // In production, implement proper refresh token storage and validation
                scope = request.scope ?? "booble_api"
            };

            return Ok(tokenResponse);
        }

        private async Task<IActionResult> HandleRefreshTokenGrant(TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.refresh_token))
            {
                return BadRequest(new { error = "invalid_request", error_description = "refresh_token is required" });
            }

            // In production, implement proper refresh token validation:
            // 1. Check if refresh token exists and is valid
            // 2. Check if refresh token is not expired
            // 3. Get the associated user
            // 4. Generate new access token
            // 5. Optionally rotate the refresh token
            
            return BadRequest(new { error = "invalid_grant", error_description = "Refresh token functionality not yet implemented" });
        }

        [HttpGet("userinfo")]
        [Authorize] // Protected endpoint - requires valid access token
        public async Task<IActionResult> UserInfo()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;
                
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { error = "invalid_token", error_description = "Invalid or missing user identifier in token" });
                }

                var user = await _usuarioService.Get(userId);
                
                if (user == null)
                {
                    return NotFound(new { error = "user_not_found", error_description = "User not found" });
                }

                // Return OpenID Connect standard userinfo response
                var userInfo = new
                {
                    sub = user.Idf_Usuario.ToString(),
                    name = user.Des_Nme,
                    email = user.Des_Email,
                    phone_number = user.Num_Telefone,
                    gender = user.Flg_Sexo?.ToString() ?? "",
                    birthdate = user.Dta_Nascimento.ToString("yyyy-MM-dd"),
                    created_at = user.Dta_Cadastro?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    updated_at = user.Dta_Alteracao?.ToString("yyyy-MM-ddTHH:mm:ssZ")
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user info for user");
                return StatusCode(500, new { error = "server_error", error_description = "An error occurred while retrieving user information" });
            }
        }

        [HttpPost("revoke")]
        [Authorize] // Protected endpoint - requires valid access token
        public IActionResult RevokeToken([FromForm] RevokeTokenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.token))
                {
                    return BadRequest(new { error = "invalid_request", error_description = "token parameter is required" });
                }

                // In production, implement proper token revocation:
                // 1. Validate the token
                // 2. Mark it as revoked in your token store/database
                // 3. If it's a refresh token, also revoke associated access tokens
                
                _logger.LogInformation("Token revocation requested - implement proper revocation logic");
                
                return Ok(new { message = "Token revocation processed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in token revocation");
                return StatusCode(500, new { error = "server_error", error_description = "An error occurred during token revocation" });
            }
        }

        [HttpGet("introspect")]
        [Authorize] // Protected endpoint for token introspection
        public IActionResult IntrospectToken([FromQuery] string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { error = "invalid_request", error_description = "token parameter is required" });
                }

                // In production, implement proper token introspection:
                // 1. Validate the token
                // 2. Return token metadata if valid
                
                return Ok(new { 
                    active = true,  // Placeholder - implement proper validation
                    token_type = "Bearer",
                    scope = "booble_api",
                    exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
                    iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in token introspection");
                return StatusCode(500, new { error = "server_error" });
            }
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