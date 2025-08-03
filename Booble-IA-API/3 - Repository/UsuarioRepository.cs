using Booble_IA_API._3___Repository.Data;
using Booble_IA_API._3___Repository.Entities;
using Booble_IA_API._3___Repository.Interfaces;
using Booble_IA_API.DTO;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Booble_IA_API._3___Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BoobleContext _boobleContext;

        public UsuarioRepository(BoobleContext context)
        {
            _boobleContext = context;
        }

        public async Task<bool> Cadastro(UsuarioDTO usuario)
        {
            try
            {
                Usuario usuarioInsert = new Usuario
                {
                    Des_Email = usuario.Des_Email.ToLower(),
                    Des_Nme = usuario.Des_Nme,
                    Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha),
                    Dta_Nascimento = usuario.Dta_Nascimento,
                    Flg_Sexo = usuario.Flg_Sexo ?? false,
                    Num_Telefone = usuario.Num_Telefone,
                    Dta_Alteracao = DateTime.UtcNow,
                    Dta_Cadastro = DateTime.UtcNow,
                };

                await _boobleContext.Usuarios.AddAsync(usuarioInsert);
                await _boobleContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao realizar cadastro: {ex.Message}.");
            }
        }

        public async Task<UsuarioDTO> GetById(int idUsuario)
        {
            try
            {
                var usuario = await _boobleContext.Usuarios.FirstOrDefaultAsync(x => x.Idf_Usuario == idUsuario);
                if (usuario == null)
                {
                    throw new Exception($"Erro ao buscar dados do usuário com Id:{idUsuario}");
                }
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
                return usuarioDTO;
            }

            catch(Exception ex)
            {
                throw new Exception($"Erro ao buscar usuário: {ex.Message}");
            }

        }

        public async Task<Usuario> Login(UsuarioDTO loginRequest)
        {
            try
            {
                var usuario = await _boobleContext.Usuarios
                    .FirstOrDefaultAsync(u => u.Des_Email == loginRequest.Des_Email.ToLower());

                if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Senha, usuario.Senha))
                    throw new Exception("Email e senha não coincidem, verifique as credenciais e tente novamente");

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao realizar login, tente novamente: {ex.Message}");
            }
        }
    }
}
