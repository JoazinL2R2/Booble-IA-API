using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;
using System.Threading.Tasks;

namespace Booble_IA_API._2___Services
{
    public class HabitoService : IHabitoService
    {
        private readonly IHabitoRepository _habitoRepository;

        public HabitoService(IHabitoRepository habitoRepository)
        {
            _habitoRepository = habitoRepository;
        }

        public async Task<bool> CadastroHabito(Habito habito)
        {
            return await _habitoRepository.CadastroHabito(habito);
        }

        public async Task<bool> FinalizarHabito(int idfHabito)
        {
            return await _habitoRepository.FinalizarHabito(idfHabito);
        }
    }
}
