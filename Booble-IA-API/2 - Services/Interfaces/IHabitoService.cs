namespace Booble_IA_API._2___Services.Interfaces
{
    using Booble_IA_API._2___Services.DTO;
    using Booble_IA_API._3___Repository.Entities;
    using System.Threading.Tasks;

    public interface IHabitoService
    {
        Task<HabitoResponseDTO?> CadastroHabito(HabitoCadastroDTO dto);
        Task<bool> FinalizarHabito(int idfHabito);
        Task<List<HabitoResponseDTO>> GetHabitosUsuario(int idUsuario);
        Task<HabitoListaResponseDTO> GetHabitosUsuarioCompleto(int idUsuario);
        Task<HabitoResponseDTO?> GetHabitoById(int idfHabito);
    }
}
