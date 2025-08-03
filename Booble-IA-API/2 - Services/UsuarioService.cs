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
                cadastroRequest.Flg_Sexo = false; //TODO: incluir flag preencher depois

            if (cadastroRequest.Dta_Nascimento == default)
                throw new ArgumentNullException(nameof(cadastroRequest.Dta_Nascimento), "O campo Data de nascimento não pode ser nulo.");

            if (string.IsNullOrEmpty(cadastroRequest.Senha))
                throw new ArgumentNullException(nameof(cadastroRequest.Senha), "O campo Senha não pode ser nula.");

            if (string.IsNullOrEmpty(cadastroRequest.Num_Telefone))
                cadastroRequest.Num_Telefone = "123123123"; //TODO: incluir flag preencher depois

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

        public async Task<UsuarioDTO> Get(int idUsuario)
        {
            return await _usuarioRepository.GetById(idUsuario);
        }

        public async Task<string> Login(UsuarioDTO loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Des_Email))
                throw new ArgumentNullException(nameof(loginRequest.Des_Email), "O campo Email não pode estar vázio.");

            if (string.IsNullOrEmpty(loginRequest.Senha))
                throw new ArgumentNullException(nameof(loginRequest.Senha), "O campo Senha não pode estar vázio.");

            Usuario usuario = await _usuarioRepository.Login(loginRequest);
            UsuarioDTO usuarioDTO = new UsuarioDTO
            {
                Idf_Usuario = usuario.Idf_Usuario,
                Des_Email = usuario.Des_Email,
                Des_Nme = usuario.Des_Nme,
                Num_Telefone = usuario.Num_Telefone,
                Flg_Sexo = usuario.Flg_Sexo,
                Dta_Nascimento = usuario.Dta_Nascimento,
                Dta_Cadastro = usuario.Dta_Cadastro,
                Dta_Alteracao = usuario.Dta_Alteracao
            };

            if (usuario != null)
            {
                return _jwtTokenService.GenerateToken(usuarioDTO);
            }

            throw new Exception("Não foi possível realizar o login");
        }

        
    }
}
