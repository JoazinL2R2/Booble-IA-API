using System.ComponentModel.DataAnnotations;

namespace Booble_IA_API._2___Services.DTO
{
    public class HabitoCadastroDTO
    {
        [Required(ErrorMessage = "ID do usuário é obrigatório")]
        public int Idf_Usuario { get; set; }

        [Required(ErrorMessage = "Descrição do hábito é obrigatória")]
        [StringLength(500, ErrorMessage = "Descrição do hábito deve ter no máximo 500 caracteres")]
        public string Des_Habito { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(100, ErrorMessage = "Título deve ter no máximo 100 caracteres")]
        public string Des_Titulo { get; set; }

        public bool? Flg_Timer { get; set; }

        public decimal? Timer_Duracao { get; set; }

        public string? Des_Icone { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "XP deve ser um valor positivo")]
        public int Num_Xp { get; set; } = 10;

        [StringLength(50, ErrorMessage = "Cor deve ter no máximo 50 caracteres")]
        public string? Des_Cor { get; set; }

        [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        public string? Des_Descricao { get; set; }

        [Required(ErrorMessage = "Frequência é obrigatória")]
        public int Idf_Frequencia { get; set; }

        // Dados opcionais para frequência personalizada
        public int? Idf_Frequencia_Personalizada { get; set; }
        public List<DateTime>? Dta_Frequencias_Personalizadas { get; set; }

        // Ícone opcional
        public int? Idf_Habito_Icone { get; set; }
    }
}
