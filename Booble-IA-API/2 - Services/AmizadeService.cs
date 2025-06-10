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
        public async Task<bool> AceitarAmizade(int idfAmizade)
        {
            try
            {
                return await _AmizadeRepository.AceitarAmizade(idfAmizade);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DesfazerAmizade
        public async Task<bool> DesfazerAmizade(int idfAmizade)
        {
            try
            {
                return await _AmizadeRepository.DesfazerAmizade(idfAmizade);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ObterAmizadePorIdUsuario
        public async Task<List<Amizade>> ObterAmizadePorIdUsuario(int idfUsuario)
        {
            try
            {
                return await _AmizadeRepository.ObterAmizadePorIdUsuario(idfUsuario);
            }
            catch
            {
                return new List<Amizade>();
            }
        }
        #endregion

        #region RecusarAmizade
        public async Task<bool> RecusarAmizade(int idfAmizade)
        {
            try
            {
                return await _AmizadeRepository.RecusarAmizade(idfAmizade);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion
    }
}
