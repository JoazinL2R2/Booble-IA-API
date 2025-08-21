using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Enums;
using Booble_IA_API._3___Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Booble_IA_API._3___Repository
{
    public class HabitoRepository : IHabitoRepository
    {
        private readonly BoobleContext _boobleContext;

        public HabitoRepository(BoobleContext context)
        {
            _boobleContext = context;
        }

        public async Task<bool> CadastroHabito(Habito habito)
        {
            try
            {
                if (habito.Dta_Conclusoes == null)
                    habito.Dta_Conclusoes = new List<DateTime>();

                habito.Frequencia.Des_Frequencia = habito.Frequencia.Idf_Frequencia switch
                {
                    1 => "Diário",
                    2 => "Semanal",
                    3 => "Mensal",
                    4 => "Personalizada",
                    _ => "Indefinida"
                };

                await _boobleContext.Habitos.AddAsync(habito);
                await _boobleContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar hábito: {ex.Message}", ex);
            }
        }

        public async Task<bool> FinalizarHabito(int idfHabito)
        {
            try
            {
                var habito = await _boobleContext.Habitos.FirstOrDefaultAsync(h => h.Idf_Habito == idfHabito);

                if (habito == null)
                    return false;

                habito.Flg_Concluido = true;
                
                // Inicializar lista se for nula
                if (habito.Dta_Conclusoes == null)
                    habito.Dta_Conclusoes = new List<DateTime>();
                
                habito.Dta_Conclusoes.Add(DateTime.UtcNow);
                
                _boobleContext.Habitos.Update(habito);
                await _boobleContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao finalizar hábito: {ex.Message}", ex);
            }
        }

        public async Task<List<Habito>> GetHabitosUsuario(int idUsuario)
        {
            try
            {
                return await _boobleContext.Habitos
                    .Include(h => h.Frequencia)
                    .Include(h => h.HabitoIcone)
                    .Include(h => h.Cor)
                    .Include(h => h.FrequenciaPersonalizada)
                    .Where(h => h.Idf_Usuario == idUsuario)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar hábitos do usuário: {ex.Message}", ex);
            }
        }

        public async Task<Habito?> GetHabitoById(int idfHabito)
        {
            try
            {
                return await _boobleContext.Habitos
                    .Include(h => h.Frequencia)
                    .Include(h => h.HabitoIcone)
                    .Include(h => h.Cor)
                    .Include(h => h.FrequenciaPersonalizada)
                    .FirstOrDefaultAsync(h => h.Idf_Habito == idfHabito);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar hábito: {ex.Message}", ex);
            }
        }
    }
}
