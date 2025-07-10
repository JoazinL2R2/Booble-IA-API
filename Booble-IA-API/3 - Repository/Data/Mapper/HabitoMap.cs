using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Booble_IA_API._3___Repository.Data.Mapper
{
    public class HabitoMap : IEntityTypeConfiguration<Habito>
    {
        public void Configure(EntityTypeBuilder<Habito> builder)
        {
            builder.ToTable("TAB_Habito");

            builder.HasKey(h => h.Idf_Habito);

            builder.Property(h => h.Idf_Habito)
                .HasColumnName("Idf_Habito")
                .ValueGeneratedOnAdd();

            builder.Property(h => h.Des_Habito)
                .HasColumnName("Des_Habito")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.Des_Titulo)
                .HasColumnName("Des_Titulo")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.Flg_Timer)
                .HasColumnName("Flg_Timer");

            builder.Property(h => h.Timer_Duracao)
                .HasColumnName("Timer_Duracao")
                .HasPrecision(10, 2);

            builder.Property(h => h.Des_Icone)
                .HasColumnName("Des_Icone")
                .HasMaxLength(100);

            builder.Property(h => h.Num_Xp)
                .HasColumnName("Num_Xp")
                .IsRequired();

            builder.Property(h => h.Flg_Concluido)
                .HasColumnName("Flg_Concluido")
                .IsRequired();

            builder.Property(h => h.Des_Cor)
                .HasColumnName("Des_Cor")
                .HasMaxLength(20);

            builder.Property(h => h.Des_Descricao)
                .HasColumnName("Des_Descricao")
                .HasMaxLength(500);

            builder.Property(h => h.Idf_Frequencia)
                .HasColumnName("Idf_Frequencia")
                .IsRequired();

            builder.Property(h => h.Dta_Cadastro)
                .HasColumnName("Dta_Cadastro")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            // Configure List<DateTime> as JSON in PostgreSQL with value comparer
            builder.Property(h => h.Dta_Conclusoes)
                .HasColumnName("Dta_Conclusoes")
                .HasColumnType("jsonb")
                .HasConversion(
                    v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => v == null ? null : JsonSerializer.Deserialize<List<DateTime>>(v, (JsonSerializerOptions?)null))
                .Metadata.SetValueComparer(new ValueComparer<List<DateTime>?>(
                    (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
                    c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c == null ? null : c.ToList()));
        }
    }
}
