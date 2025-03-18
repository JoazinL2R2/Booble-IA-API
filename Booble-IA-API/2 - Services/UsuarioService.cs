using System.Runtime.CompilerServices;
using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository;
using Booble_IA_API.DTO;
using Newtonsoft.Json.Linq;

namespace Booble_IA_API._2___Services
{
    public class UsuarioService : IUsuarioService
    {
        #region Construtores
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenService _jwtTokenService;
        public UsuarioService(IUsuarioRepository usuarioRepository, IJwtTokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenService = tokenService;
        }
        #endregion

        #region Metodos

        #region Cadastro
        public async Task<object> Cadastro(UsuarioDTO cadastroRequest)
        {
            if (cadastroRequest != null)
            {
                if(string.IsNullOrEmpty(cadastroRequest.Des_Email))
                    throw new ArgumentNullException(nameof(cadastroRequest.Des_Email), "O campo Email não pode ser nulo.");

                if(string.IsNullOrEmpty(cadastroRequest.Des_Nme))
                    throw new ArgumentNullException(nameof(cadastroRequest.Des_Email), "O campo Nome não pode ser nulo.");

                if (string.IsNullOrEmpty(cadastroRequest.Flg_Sexo.ToString()))
                    throw new ArgumentNullException(nameof(cadastroRequest.Des_Email), "O campo Sexo não pode ser nulo.");

                if (string.IsNullOrEmpty(cadastroRequest.Dta_Nascimento.ToString()))
                    throw new ArgumentNullException(nameof(cadastroRequest.Des_Email), "O campo Data de nascimento não pode ser nulo.");

                if (string.IsNullOrEmpty(cadastroRequest.Senha))
                    throw new ArgumentNullException(nameof(cadastroRequest.Des_Email), "A campo Senha não pode ser nula.");

                if (string.IsNullOrEmpty(cadastroRequest.Num_Telefone))
                    throw new ArgumentNullException(nameof(cadastroRequest.Des_Email), "A campo Numero de telefone não pode ser nulo.");

                if(await _usuarioRepository.Cadastro(cadastroRequest))
                {
                    string token = _jwtTokenService.GenerateToken(cadastroRequest);

                    if (string.IsNullOrEmpty(token))
                        throw new Exception("Erro ao gerar token de autenticação.");

                    return new
                    {
                        usuario = cadastroRequest,
                        Authtoken = token
                    };
                }
            }
            throw new ArgumentNullException(nameof(cadastroRequest),"Null request");
        }
        #endregion

        #region Login
        public async Task<bool> Login(UsuarioDTO loginRequest)
        {
            try
            {
                if(string.IsNullOrEmpty(loginRequest.Des_Email))
                    throw new ArgumentNullException(nameof(loginRequest.Des_Email),"O campo Email não pode estar vázio.");

                if (string.IsNullOrEmpty(loginRequest.Senha))
                    throw new ArgumentNullException(nameof(loginRequest.Senha), "O campo Email não pode estar vázio.");
                
                if(await _usuarioRepository.Login(loginRequest))
                {
                    string token = _jwtTokenService.GenerateToken(cadastroRequest);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #endregion
    }
}
