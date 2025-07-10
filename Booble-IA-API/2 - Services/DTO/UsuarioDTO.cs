using Booble_IA_API._3___Repository.Entities;

namespace Booble_IA_API.DTO
{
    public class UsuarioDTO
    {
        //public UsuarioDTO(Usuario usuario)
        //{
        //    Idf_Usuario = usuario.Idf_Usuario;
        //    Des_Email = usuario.Des_Email;
        //    Des_Nme = usuario.Des_Nme;
        //    Num_Telefone = usuario.Num_Telefone;
        //    Senha = usuario.Senha; 
        //    Flg_Sexo = usuario.Flg_Sexo;
        //    Dta_Nascimento = usuario.Dta_Nascimento;
        //    Dta_Cadastro = usuario.Dta_Cadastro; 
        //    Dta_Alteracao = usuario.Dta_Alteracao;
        //}

        public int? Idf_Usuario { get; set; }
        public string? Des_Email { get; set; }
        public string? Des_Nme { get; set; }
        public string? Num_Telefone { get; set; }
        public string? Senha { get; set; }
        public bool? Flg_Sexo { get; set; }
        public DateTime Dta_Nascimento { get; set; }
        public DateTime? Dta_Cadastro { get; set; }
        public DateTime? Dta_Alteracao { get; set; }
    }
}
