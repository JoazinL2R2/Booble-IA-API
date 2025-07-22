using Booble_IA_API._3___Repository.Enums;

namespace Booble_IA_API._2___Services.DTO
{
    public class HabitoCadastroDTO
    {
        public string Des_Habito { get; set; }
        public string Des_Titulo { get; set; }
        public bool? Flg_Timer { get; set; }
        public decimal? Timer_Duracao { get; set; }
        public string Des_Icone { get; set; }
        public int Num_Xp { get; set; }
        public string Des_Cor { get; set; }
        public string Des_Descricao { get; set; }
        public FrequenciaEnum Idf_Frequencia { get; set; }
        public int Idf_Usuario { get; set; }
    }
}
