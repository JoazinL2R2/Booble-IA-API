using Booble_IA_API._2___Services.Interfaces;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;
using Booble_IA_API.DTO;
using System;
using System.Threading.Tasks;

namespace Booble_IA_API._2___Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IJwtTokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenService = tokenService;
        }

        public async Task<object> Cadastro(UsuarioDTO cadastroRequest)
        {
            if (cadastroRequest == null)
                throw new ArgumentNullException(nameof(cadastroRequest), "Null request");

            if (string.IsNullOrEmpty(cadastroRequest.Des_Email))
                throw new ArgumentNullException(nameof(cadastroRequest.Des_Email), "O campo Email não pode ser nulo.");

            if (string.IsNullOrEmpty(cadastroRequest.Des_Nme))
                throw new ArgumentNullException(nameof(cadastroRequest.Des_Nme), "O campo Nome não pode ser nulo.");

            if (!cadastroRequest.Flg_Sexo.HasValue)
                throw new ArgumentNullException(nameof(cadastroRequest.Flg_Sexo), "O campo Sexo não pode ser nulo.");

            if (cadastroRequest.Dta_Nascimento == default)
                throw new ArgumentNullException(nameof(cadastroRequest.Dta_Nascimento), "O campo Data de nascimento não pode ser nulo.");

            if (string.IsNullOrEmpty(cadastroRequest.Senha))
                throw new ArgumentNullException(nameof(cadastroRequest.Senha), "O campo Senha não pode ser nula.");

            if (string.IsNullOrEmpty(cadastroRequest.Num_Telefone))
                throw new ArgumentNullException(nameof(cadastroRequest.Num_Telefone), "O campo Numero de telefone não pode ser nulo.");

            if (await _usuarioRepository.Cadastro(cadastroRequest))
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

            throw new Exception("Erro ao cadastrar usuário.");
        }

        public async Task<string> Login(UsuarioDTO loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Des_Email))
                throw new ArgumentNullException(nameof(loginRequest.Des_Email), "O campo Email não pode estar vázio.");

            if (string.IsNullOrEmpty(loginRequest.Senha))
                throw new ArgumentNullException(nameof(loginRequest.Senha), "O campo Senha não pode estar vázio.");

            Usuario usuario = await _usuarioRepository.Login(loginRequest);
            UsuarioDTO usuarioDTO = new UsuarioDTO(usuario);

            if (usuario != null)
            {
                return _jwtTokenService.GenerateToken(usuarioDTO);
            }

            throw new Exception("Não foi possível realizar o login");
        }
    }
}
