using Booble_IA_API._2___Services.DTO;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Enums;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    
                    return BadRequest(ApiResponseDTO<object>.CriarErro("Dados inválidos", errors));
                }

                var resultado = await _habitoService.CadastroHabito(dto);
                
                if (resultado != null)
                {
                    return CreatedAtAction(
                        nameof(GetHabitoById), 
                        new { id = resultado.Idf_Habito }, 
                        ApiResponseDTO<HabitoResponseDTO>.CriarSucesso(resultado, "Hábito cadastrado com sucesso!")
                    );
                }
                
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno ao cadastrar hábito"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponseDTO<object>.CriarErro(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }

        [HttpPost("finalizar")]
        public async Task<IActionResult> FinalizarHabito([FromBody] HabitoFinalizarDTO dto)
        {
            try
            {
                if (dto?.Idf_Habito <= 0)
                    return BadRequest(ApiResponseDTO<object>.CriarErro("ID do hábito inválido"));

                var resultado = await _habitoService.FinalizarHabito(dto.Idf_Habito);
                
                if (resultado)
                    return Ok(ApiResponseDTO<object>.CriarSucesso(null, "Hábito finalizado com sucesso!"));
                
                return NotFound(ApiResponseDTO<object>.CriarErro("Hábito não encontrado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetHabitosUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                    return BadRequest(ApiResponseDTO<object>.CriarErro("ID do usuário inválido"));

                var resultado = await _habitoService.GetHabitosUsuarioCompleto(idUsuario);
                
                if (resultado.TotalHabitos > 0)
                {
                    return Ok(ApiResponseDTO<HabitoListaResponseDTO>.CriarSucesso(
                        resultado, 
                        $"Encontrados {resultado.TotalHabitos} hábitos")
                    );
                }
                
                return Ok(ApiResponseDTO<HabitoListaResponseDTO>.CriarSucesso(
                    resultado, 
                    "Nenhum hábito encontrado. Que tal cadastrar o primeiro?")
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabitoById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(ApiResponseDTO<object>.CriarErro("ID do hábito inválido"));

                var habito = await _habitoService.GetHabitoById(id);
                
                if (habito != null)
                    return Ok(ApiResponseDTO<HabitoResponseDTO>.CriarSucesso(habito, "Hábito encontrado"));
                
                return NotFound(ApiResponseDTO<object>.CriarErro("Hábito não encontrado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }

        [HttpGet("usuario/{idUsuario}/resumo")]
        public async Task<IActionResult> GetResumoHabitos(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                    return BadRequest(ApiResponseDTO<object>.CriarErro("ID do usuário inválido"));

                var resultado = await _habitoService.GetHabitosUsuarioCompleto(idUsuario);
                
                var resumo = new 
                {
                    TotalHabitos = resultado.TotalHabitos,
                    HabitosConcluidos = resultado.HabitosConcluidos,
                    HabitosPendentes = resultado.HabitosPendentes,
                    PercentualConclusao = resultado.TotalHabitos > 0 
                        ? Math.Round((double)resultado.HabitosConcluidos / resultado.TotalHabitos * 100, 2)
                        : 0
                };

                return Ok(ApiResponseDTO<object>.CriarSucesso(resumo, "Resumo dos hábitos"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }

        [HttpGet("cores-disponiveis")]
        public IActionResult GetCoresDisponiveis()
        {
            try
            {
                // Lista das cores padrão disponíveis no sistema
                // Essas cores devem estar cadastradas na tabela TAB_Cor
                var coresDisponiveis = new List<string>
                {
                    "Azul",
                    "Verde",
                    "Vermelho",
                    "Amarelo",
                    "Roxo",
                    "Laranja",
                    "Rosa",
                    "Cinza",
                    "Marrom",
                    "Preto"
                };

                return Ok(ApiResponseDTO<List<string>>.CriarSucesso(
                    coresDisponiveis,
                    "Cores disponíveis para cadastro de hábito")
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }

        [HttpGet("exemplo-cadastro")]
        public IActionResult GetExemploCadastro()
        {
            try
            {
                var exemplo = new HabitoCadastroDTO
                {
                    Idf_Usuario = 1,
                    Des_Habito = "Exercitar-se regularmente",
                    Des_Titulo = "Exercícios Matinais",
                    Flg_Timer = true,
                    Timer_Duracao = 30.0m, // 30 minutes as decimal
                    Des_Descricao = "Fazer exercícios todas as manhãs para manter a forma física",
                    Idf_Frequencia = (int)FrequenciaEnum.Diariamente, // Cast enum to int
                    Num_Xp = 50, // Set as integer value
                    Des_Cor = "Verde", // Color description that should exist in TAB_Cor
                    Des_Icone = "Exercicio", // Icon description that should exist in TAB_Habito_Icone
                    Idf_Habito_Icone = 1 // Optional icon ID
                };

                return Ok(ApiResponseDTO<HabitoCadastroDTO>.CriarSucesso(
                    exemplo, 
                    "Exemplo de cadastro de hábito - Use o endpoint /cores-disponiveis para ver as cores válidas")
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseDTO<object>.CriarErro("Erro interno do servidor", new List<string> { ex.Message }));
            }
        }
    }
}
