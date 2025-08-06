using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet("streak")]
        public async Task<ActionResult<List<RankingStreakDTO>>> GetRankingStreak()
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

                var ranking = await _rankingService.GetRankingStreakAsync();
                if (ranking == null || ranking.Count == 0)
                    return NotFound(new { message = "Nenhum ranking encontrado." });
                
                return Ok(ranking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Additional endpoint to get user's position in ranking
        [HttpGet("streak/posicao")]
        public async Task<ActionResult> GetPosicaoUsuarioRanking()
        {
            try
            {
                // Get the authenticated user ID from token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                // Here you would implement logic to get user's ranking position
                // For now, return placeholder
                return Ok(new { 
                    message = "Posição do usuário no ranking - implementar lógica",
                    userId = userId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
