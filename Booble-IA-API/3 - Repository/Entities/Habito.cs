using Booble_IA_API._3___Repository.Enums;

namespace Booble_IA_API._3___Repository.Entities
{
    public class Habito
    {
        public int Idf_Habito { get; set; }
        public string Des_Habito { get; set; }
        public string Des_Titulo { get; set; }
        public bool? Flg_Timer { get; set; }
        public decimal? Timer_Duracao { get; set; }
        public string Des_Icone { get; set; }
        public int Num_Xp { get; set; }
        public bool Flg_Concluido { get; set; }
        public string Des_Cor { get; set; }
        public string Des_Descricao { get; set; }
        public FrequenciaEnum Idf_Frequencia { get; set; }
        public DateTime Dta_Cadastro { get; set; }
        public List<DateTime>? Dta_Conclusoes { get; set; }

        public Habito()
        {

        }
    }
}
