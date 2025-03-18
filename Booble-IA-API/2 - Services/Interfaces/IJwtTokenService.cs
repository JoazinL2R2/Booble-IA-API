using Booble_IA_API.DTO;

namespace Booble_IA_API._2___Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(UsuarioDTO usuario);
    }
}
