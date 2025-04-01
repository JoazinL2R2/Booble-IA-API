namespace Booble_IA_API._3___Repository.Entities
{
    public class Amizade
    {
        public int Idf_Amizade { get; set; }
        public int Idf_Usuario_Solicitante { get; set; }
        public int Idf_Usuario_Recebedor { get; set; }
        public DateTime Dta_Cadastro { get; set; }
        public bool Flg_Aceito { get; set; }
        public bool Flg_Inativo { get; set; }

    }
}
