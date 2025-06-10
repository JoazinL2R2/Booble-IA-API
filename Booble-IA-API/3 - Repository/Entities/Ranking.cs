namespace Booble_IA_API._3___Repository.Entities
{
    public class Ranking
    {
        public int Idf_Usuario { get; set; }
        public int Num_Posicao { get; set; }
        public int Num_Streak { get; set; }
        public decimal Num_Xp { get; set; }
        public int Num_lvl { get; set; }
        public DateTime Dta_Atualizacao { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();

    }
}
