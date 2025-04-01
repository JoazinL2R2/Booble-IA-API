using Booble_IA_API._3___Repository.Entities;

namespace Booble_IA_API._2___Services.Interfaces
{
    public interface IAmizadeService
    {
        Task<List<Amizade>> ObterAmizadePorIdUsuario(int idfUsuario);
        Task<bool> AceitarAmizade(int idfAmizade);
        Task<bool> RecusarAmizade(int idfAmizade);
        Task<bool> DesfazerAmizade(int idfAmizade);
    }
}
