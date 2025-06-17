using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;
using Booble_IA_API.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
                habito.Dta_Cadastro = DateTime.Now;
                habito.Flg_Concluido = false;
                await _boobleContext.Habitos.AddAsync(habito);
                await _boobleContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar hábito: {ex.Message}.");
            }
        }

        public async Task<bool> FinalizarHabito(int idfHabito)
        {
            try
            {
                var habito = await _boobleContext.Habitos.FirstOrDefaultAsync(h => h.Idf_Habito == idfHabito);

                if (habito == null)
                    throw new Exception("Hábito não encontrado ou inexistente");

                habito.Flg_Concluido = true;
                habito.Dta_Conclusoes.Add(DateTime.Now);
                _boobleContext.Habitos.Update(habito);
                await _boobleContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao finalizar hábito: {ex.Message}.");
            }
        }
    }
}
