namespace Booble_IA_API._3___Repository.Entities
{
    public class Usuario
    {
        public int Idf_Usuario { get; set; }
        public string Des_Email { get; set; }
        public string Des_Nme { get; set; }
        public string Num_Telefone { get; set; }
        public string Senha { get; set; }
        public bool Flg_Sexo { get; set; }
        public DateTime Dta_Cadastro { get; set; }
        public DateTime Dta_Nascimento { get; set; }
        public DateTime Dta_Alteracao { get; set; }
        public Usuario()
        {

        }
    }
}
