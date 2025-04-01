using System.Runtime.CompilerServices;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API.DTO;

namespace Booble_IA_API._2___Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> Cadastro(UsuarioDTO cadastroRequest);
        Task<Usuario> Login(UsuarioDTO loginRequest);
    }
}
