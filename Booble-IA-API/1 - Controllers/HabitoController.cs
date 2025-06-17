using Booble_IA_API._2___Services.DTO;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booble_IA_API._1___Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var habito = new Habito
            {
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
                return Ok("Hábito cadastrado com sucesso.");
            else
                return StatusCode(500, "Erro ao cadastrar hábito.");
        }

        [HttpPost("finalizar")]
        public async Task<IActionResult> FinalizarHabito([FromBody] HabitoFinalizarDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var result = await _habitoService.FinalizarHabito(dto.Idf_Habito);
            if (result)
                return Ok("Hábito finalizado com sucesso.");
            else
                return NotFound("Hábito não encontrado ou já finalizado.");
        }
    }
}
