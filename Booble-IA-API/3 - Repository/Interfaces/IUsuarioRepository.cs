using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API.DTO;
using System.Threading.Tasks;

namespace Booble_IA_API._3___Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> Cadastro(UsuarioDTO usuario);
        Task<Usuario> Login(UsuarioDTO loginRequest);
        Task<UsuarioDTO> GetById(int idUsuario);
    }
}
