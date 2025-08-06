using Booble_IA_API._2___Services.DTO;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Booble_IA_API._1___Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protect all endpoints in this controller
    public class HabitoController : ControllerBase
    {
        private readonly IHabitoService _habitoService;

        public HabitoController(IHabitoService habitoService)
        {
            _habitoService = habitoService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastroHabito([FromBody] HabitoCadastroDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Dados inválidos." });

                // Get the authenticated user ID from token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var authenticatedUserId))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                // Ensure the user can only create habits for themselves
                if (dto.Idf_Usuario != authenticatedUserId)
                {
                    return Forbid("Você só pode criar hábitos para si mesmo.");
                }

                var habito = new Habito
                {
                    Idf_Usuario = dto.Idf_Usuario,
                    Des_Habito = dto.Des_Habito,
                    Des_Titulo = dto.Des_Titulo,
                    Flg_Timer = dto.Flg_Timer,
                    Timer_Duracao = dto.Timer_Duracao,
                    Des_Icone = dto.Des_Icone,
                    Num_Xp = dto.Num_Xp,
                    Des_Cor = dto.Des_Cor,
                    Des_Descricao = dto.Des_Descricao,
                    Idf_Frequencia = dto.Idf_Frequencia,
                    Dta_Cadastro = DateTime.UtcNow,
                    Flg_Concluido = false
                };

                var result = await _habitoService.CadastroHabito(habito);
                if (result)
                    return Ok(new { message = "Hábito cadastrado com sucesso." });
                else
                    return StatusCode(500, new { message = "Erro ao cadastrar hábito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpPost("finalizar")]
        public async Task<IActionResult> FinalizarHabito([FromBody] HabitoFinalizarDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Dados inválidos." });

                // Get the authenticated user ID from token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                                User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var authenticatedUserId))
                {
                    return Unauthorized(new { message = "Token inválido." });
                }

                // Here you should add logic to verify the habit belongs to the authenticated user
                // For now, we'll proceed with the finalization
                var result = await _habitoService.FinalizarHabito(dto.Idf_Habito);
                if (result)
                    return Ok(new { message = "Hábito finalizado com sucesso." });
                else
                    return NotFound(new { message = "Hábito não encontrado ou já finalizado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // GET: api/Habito/usuario/{userId} - Get habits for a specific user
        [HttpGet("usuario/{userId}")]
        public async Task<IActionResult> GetHabitosPorUsuario(int userId)
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

                // Users can only access their own habits
                if (authenticatedUserId != userId)
                {
                    return Forbid("Você só pode acessar seus próprios hábitos.");
                }

                // Here you would implement the service method to get user habits
                // For now, return a placeholder
                return Ok(new { message = "Endpoint para buscar hábitos do usuário - implementar service method" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
