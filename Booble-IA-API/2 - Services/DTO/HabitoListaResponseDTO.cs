namespace Booble_IA_API._2___Services.DTO
{
    public class HabitoListaResponseDTO
    {
        public List<HabitoResponseDTO> Habitos { get; set; }
        public int TotalHabitos { get; set; }
        public int HabitosConcluidos { get; set; }
        public int HabitosPendentes { get; set; }

        public HabitoListaResponseDTO()
        {
            Habitos = new List<HabitoResponseDTO>();
        }

        public HabitoListaResponseDTO(List<HabitoResponseDTO> habitos)
        {
            Habitos = habitos ?? new List<HabitoResponseDTO>();
            TotalHabitos = Habitos.Count;
            HabitosConcluidos = Habitos.Count(h => h.Flg_Concluido);
            HabitosPendentes = TotalHabitos - HabitosConcluidos;
        }
    }
}