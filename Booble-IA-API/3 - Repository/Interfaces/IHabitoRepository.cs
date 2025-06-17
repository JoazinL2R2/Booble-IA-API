using Booble_IA_API._3___Repository.Entities;

namespace Booble_IA_API._3___Repository.Interfaces
{
    public interface IHabitoRepository
    {
        Task<bool> CadastroHabito(Habito habito);
        Task<bool> FinalizarHabito(int idfHabito);
    }
}
