namespace Booble_IA_API._2___Services.Interfaces
{
    using Booble_IA_API._3___Repository.Entities;
    using System.Threading.Tasks;

    public interface IHabitoService
    {
        Task<bool> CadastroHabito(Habito habito);
        Task<bool> FinalizarHabito(int idfHabito);
    }
}
