using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Extensions;
using Booble_IA_API._3___Repository.Interfaces;
using System.Security.Claims;
using Booble_IA_API.DTO;

namespace Booble_IA_API._2___Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CustomProfileService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            
            if (int.TryParse(userId, out var userIdInt))
            {
                var user = await _usuarioRepository.GetById(userIdInt);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Idf_Usuario.ToString() ?? ""),
                        new Claim(ClaimTypes.Name, user.Des_Nme ?? ""),
                        new Claim(ClaimTypes.Email, user.Des_Email ?? ""),
                        new Claim("phone", user.Num_Telefone ?? ""),
                        new Claim("gender", user.Flg_Sexo?.ToString() ?? ""),
                        new Claim("birthdate", user.Dta_Nascimento.ToString("yyyy-MM-dd"))
                    };

                    context.IssuedClaims = claims;
                }
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            
            if (int.TryParse(userId, out var userIdInt))
            {
                var user = await _usuarioRepository.GetById(userIdInt);
                context.IsActive = user != null;
            }
            else
            {
                context.IsActive = false;
            }
        }
    }
}