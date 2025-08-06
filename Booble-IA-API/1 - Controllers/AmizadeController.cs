using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Booble_IA_API._1___Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protect all endpoints in this controller
    public class AmizadeController : ControllerBase
    {
        private readonly IAmizadeService _amizadeService;

        public AmizadeController(IAmizadeService amizadeService)
        {
            _amizadeService = amizadeService;
        }

        // GET: api/Amizade/usuario?idfUsuario=5
        [HttpGet("usuario")]
        public async Task<ActionResult<List<Amizade>>> ObterAmizadePorIdUsuario([FromQuery] int idfUsuario)
        {
            try
            {
                // Get the authenticated user ID from token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var authenticatedUserId))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                // Users can only access their own friendships or friends' friendships
                // For now, let's allow access to own friendships only
                if (authenticatedUserId != idfUsuario)
                {
                    return Forbid("Você só pode acessar suas próprias amizades.");
                }

                var amizades = await _amizadeService.ObterAmizadePorIdUsuario(idfUsuario);
                if (amizades == null || amizades.Count == 0)
                    return NotFound(new { message = "Nenhuma amizade encontrada." });
                
                return Ok(amizades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // POST: api/Amizade/aceitar?idfAmizade=5
        [HttpPost("aceitar")]
        public async Task<ActionResult> AceitarAmizade([FromQuery] int idfAmizade)
        {
            try
            {
                // Verify user authentication
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                var result = await _amizadeService.AceitarAmizade(idfAmizade);
                if (!result)
                    return BadRequest(new { message = "Não foi possível aceitar a amizade." });
                
                return Ok(new { message = "Amizade aceita com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // POST: api/Amizade/recusar?idfAmizade=5
        [HttpPost("recusar")]
        public async Task<ActionResult> RecusarAmizade([FromQuery] int idfAmizade)
        {
            try
            {
                // Verify user authentication
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                var result = await _amizadeService.RecusarAmizade(idfAmizade);
                if (!result)
                    return BadRequest(new { message = "Não foi possível recusar a amizade." });
                
                return Ok(new { message = "Amizade recusada com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // POST: api/Amizade/desfazer?idfAmizade=5
        [HttpPost("desfazer")]
        public async Task<ActionResult> DesfazerAmizade([FromQuery] int idfAmizade)
        {
            try
            {
                // Verify user authentication
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                var result = await _amizadeService.DesfazerAmizade(idfAmizade);
                if (!result)
                    return BadRequest(new { message = "Não foi possível desfazer a amizade." });
                
                return Ok(new { message = "Amizade desfeita com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}