using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Booble_IA_API._3___Repository
{
    public class AmizadeRepository : IAmizadeRepository
    {
        #region construtores
        private readonly BoobleContext _boobleContext;
        public AmizadeRepository(BoobleContext boobleContext)
        {
            _boobleContext = boobleContext;
        }
        #endregion

        #region Metodos

        #region AceitarAmizade
        public async Task<bool> AceitarAmizade(int idfAmizade)
        {
            Amizade amizade = await _boobleContext.Amizades.FirstOrDefaultAsync(x => x.Idf_Amizade == idfAmizade);
            if (amizade == null)
                throw new Exception("Amizade não encontrada ou inexistente");
            amizade.Flg_Aceito = true;
            _boobleContext.Amizades.Update(amizade);
            await _boobleContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region DesfazerAmizade
        public async Task<bool> DesfazerAmizade(int idfAmizade)
        {
            Amizade amizade = await _boobleContext.Amizades.FirstOrDefaultAsync(x => x.Idf_Amizade == idfAmizade);

            if (amizade == null)
                throw new Exception("Amizade não encontrada ou inexistente");

            amizade.Flg_Inativo = false;
            amizade.Flg_Aceito = false;
            _boobleContext.Amizades.Update(amizade);
            await _boobleContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region ObterAmizadePorIdUsuario
        public async Task<List<Amizade>> ObterAmizadePorIdUsuario(int idfUsuario)
        {
            Usuario usuario = await _boobleContext.Usuarios.FirstOrDefaultAsync(x => x.Idf_Usuario == idfUsuario);

            if (usuario.Idf_Usuario == 0 || usuario.Idf_Usuario == null)
                throw new Exception("Usuario não encontrado ou inexistente");

            return await _boobleContext.Amizades
                .Where(x => x.Idf_Usuario_Solicitante == usuario.Idf_Usuario &&
                            x.Flg_Inativo == false &&
                            x.Flg_Inativo == false)
                .Select(x => new Amizade
                {
                    Idf_Amizade = x.Idf_Amizade,
                    Idf_Usuario_Solicitante = x.Idf_Usuario_Solicitante,
                    Idf_Usuario_Recebedor = x.Idf_Usuario_Recebedor,
                    Dta_Cadastro = x.Dta_Cadastro,
                })
                .ToListAsync();
        }
        #endregion

        #region RecusarAmizade
        public async Task<bool> RecusarAmizade(int idfAmizade)
        {
            Amizade amizade = await _boobleContext.Amizades.FirstOrDefaultAsync(x => x.Idf_Amizade == idfAmizade);
            if (amizade == null) 
                throw new Exception("Amizade inexistente ou não encontrada");

            amizade.Flg_Aceito = false;
            _boobleContext.Amizades.Update(amizade);
            await _boobleContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #endregion
    }
}
