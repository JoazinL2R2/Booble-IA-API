using Booble_IA_API._2___Services.DTO;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;

namespace Booble_IA_API._2___Services
{
    public class HabitoService : IHabitoService
    {
        private readonly IHabitoRepository _habitoRepository;

        public HabitoService(IHabitoRepository habitoRepository)
        {
            _habitoRepository = habitoRepository;
        }

        public async Task<HabitoResponseDTO?> CadastroHabito(HabitoCadastroDTO dto)
        {
            try
            {
                // Validações de negócio
                if (dto.Timer_Duracao.HasValue && dto.Timer_Duracao <= 0)
                    throw new ArgumentException("Duração do timer deve ser maior que zero");

                if (dto.Flg_Timer == true && !dto.Timer_Duracao.HasValue)
                    throw new ArgumentException("Duração do timer é obrigatória quando timer está habilitado");

                // Validação da cor (se fornecida)
                if (!string.IsNullOrWhiteSpace(dto.Des_Cor))
                {
                    ValidarCorDisponivel(dto.Des_Cor);
                }

                // Criar entidade
                var habito = new Habito
                {
                    Idf_Usuario = dto.Idf_Usuario,
                    Des_Habito = dto.Des_Habito,
                    Des_Titulo = dto.Des_Titulo,
                    Flg_Timer = dto.Flg_Timer ?? false,
                    Timer_Duracao = dto.Timer_Duracao,
                    Des_Icone = dto.Des_Icone,
                    Num_Xp = dto.Num_Xp,
                    Des_Cor = dto.Des_Cor,
                    Des_Descricao = dto.Des_Descricao,
                    Idf_Frequencia = dto.Idf_Frequencia,
                    Dta_Cadastro = DateTime.UtcNow,
                    Flg_Concluido = false,
                    Dta_Conclusoes = new List<DateTime>()
                };

                // Criar frequência personalizada se fornecida
                if (dto.Idf_Frequencia_Personalizada.HasValue && dto.Dta_Frequencias_Personalizadas?.Any() == true)
                {
                    habito.FrequenciaPersonalizada = new FrequenciaPersonalizada
                    {
                        Idf_Frequencia = dto.Idf_Frequencia,
                        Des_Frequencia_Personalizada = dto.Idf_Frequencia_Personalizada.Value,
                        Dta_Frequencias = dto.Dta_Frequencias_Personalizadas,
                        Dta_Cadastro = DateTime.UtcNow
                    };
                }

                var sucesso = await _habitoRepository.CadastroHabito(habito);
                
                if (sucesso)
                {
                    // Buscar o hábito recém-criado com suas relações
                    var habitoCompleto = await _habitoRepository.GetHabitoById(habito.Idf_Habito);
                    return habitoCompleto != null ? MapearParaResponseDTO(habitoCompleto) : null;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> FinalizarHabito(int idfHabito)
        {
            return await _habitoRepository.FinalizarHabito(idfHabito);
        }

        public async Task<List<HabitoResponseDTO>> GetHabitosUsuario(int idUsuario)
        {
            var habitos = await _habitoRepository.GetHabitosUsuario(idUsuario);
            return habitos.Select(MapearParaResponseDTO).ToList();
        }

        public async Task<HabitoListaResponseDTO> GetHabitosUsuarioCompleto(int idUsuario)
        {
            var habitos = await GetHabitosUsuario(idUsuario);
            return new HabitoListaResponseDTO(habitos);
        }

        public async Task<HabitoResponseDTO?> GetHabitoById(int idfHabito)
        {
            var habito = await _habitoRepository.GetHabitoById(idfHabito);
            return habito != null ? MapearParaResponseDTO(habito) : null;
        }

        private HabitoResponseDTO MapearParaResponseDTO(Habito habito)
        {
            return new HabitoResponseDTO
            {
                Idf_Habito = habito.Idf_Habito,
                Des_Habito = habito.Des_Habito,
                Des_Titulo = habito.Des_Titulo,
                Flg_Timer = habito.Flg_Timer,
                Timer_Duracao = habito.Timer_Duracao,
                Des_Icone = habito.Des_Icone,
                Num_Xp = habito.Num_Xp,
                Flg_Concluido = habito.Flg_Concluido,
                Des_Cor = habito.Des_Cor,
                Des_Descricao = habito.Des_Descricao,
                Idf_Frequencia = habito.Idf_Frequencia,
                Dta_Cadastro = habito.Dta_Cadastro,
                Dta_Conclusoes = habito.Dta_Conclusoes,
                Nome_Frequencia = habito.Frequencia?.Des_Frequencia,
                Nome_Icone = habito.HabitoIcone?.Des_Habito_Icone
            };
        }

        private void ValidarCorDisponivel(string desCor)
        {
            // Lista das cores válidas que devem existir na tabela TAB_Cor
            var coresValidas = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Azul", "Verde", "Vermelho", "Amarelo", "Roxo", 
                "Laranja", "Rosa", "Cinza", "Marrom", "Preto"
            };

            if (!coresValidas.Contains(desCor))
            {
                throw new ArgumentException($"Cor '{desCor}' não é válida. Use o endpoint /api/habito/cores-disponiveis para ver as cores disponíveis.");
            }
        }
    }
}
