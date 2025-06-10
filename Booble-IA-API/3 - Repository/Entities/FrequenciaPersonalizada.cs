namespace Booble_IA_API._3___Repository.Entities
{
    public class FrequenciaPersonalizada
    {
        public int Idf_Frequencia_Personalizada { get; set; }
        public int Idf_Frequencia { get; set; }
        public int Des_Frequencia_Personalizada { get; set; }
        public List<DateTime> Dta_Frequencias { get; set; } = new List<DateTime>();
        public DateTime Dta_Cadastro { get; set; }
    }
}
