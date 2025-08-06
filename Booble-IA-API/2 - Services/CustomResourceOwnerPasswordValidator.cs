using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Booble_IA_API._3___Repository.Interfaces;
using Booble_IA_API.DTO;
using System.Security.Claims;

namespace Booble_IA_API._2___Services
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CustomResourceOwnerPasswordValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var loginDto = new UsuarioDTO
                {
                    Des_Email = context.UserName,
                    Senha = context.Password
                };

                var user = await _usuarioRepository.Login(loginDto);

                if (user != null)
                {
                    context.Result = new GrantValidationResult(
                        subject: user.Idf_Usuario.ToString(),
                        authenticationMethod: "password",
                        claims: new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Idf_Usuario.ToString()),
                            new Claim(ClaimTypes.Name, user.Des_Nme ?? ""),
                            new Claim(ClaimTypes.Email, user.Des_Email ?? ""),
                            new Claim("phone", user.Num_Telefone ?? ""),
                            new Claim("gender", user.Flg_Sexo.ToString()),
                            new Claim("birthdate", user.Dta_Nascimento.ToString("yyyy-MM-dd"))
                        }
                    );
                }
                else
                {
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "Invalid credentials"
                    );
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    $"Authentication failed: {ex.Message}"
                );
            }
        }
    }
}