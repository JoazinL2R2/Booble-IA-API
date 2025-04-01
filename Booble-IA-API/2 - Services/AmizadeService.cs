using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository;
using Booble_IA_API._3___Repository.Entities;

namespace Booble_IA_API._2___Services
{
    public class AmizadeService : IAmizadeService
    {
        #region Construtores
        private readonly AmizadeRepository _AmizadeRepository;
        public AmizadeService(AmizadeRepository amizadeRepository)
        {
            _AmizadeRepository = amizadeRepository;
        }
        #endregion

        #region Metodos

        #region AceitarAmizade
        public Task<bool> AceitarAmizade(int idfAmizade)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DesfazerAmizade
        public Task<bool> DesfazerAmizade(int idfAmizade)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ObterAmizadePorIdUsuario
        public Task<List<Amizade>> ObterAmizadePorIdUsuario(int idfUsuario)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region RecusarAmizade
        public Task<bool> RecusarAmizade(int idfAmizade)
        {
            throw new NotImplementedException();
        }
        #endregion


        #endregion
    }
}
