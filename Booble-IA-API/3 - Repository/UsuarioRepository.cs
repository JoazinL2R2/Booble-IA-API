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
                    Dta_Alteracao = DateTime.Now,
                    Dta_Cadastro = DateTime.Now,
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
