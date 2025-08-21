using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API.DTO;
using System.Threading.Tasks;

namespace Booble_IA_API._2___Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<object> Cadastro(UsuarioDTO cadastroRequest);
        Task<UsuarioDTO> Login(UsuarioDTO loginRequest);

        Task<UsuarioDTO> Get(int idUsuario);
    }
}
