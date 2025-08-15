using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Booble_IA_API._3___Repository.Data.Mapper
{
    public class FrequenciaPersonalizadaMap : IEntityTypeConfiguration<FrequenciaPersonalizada>
    {
        public void Configure(EntityTypeBuilder<FrequenciaPersonalizada> builder)
        {
            builder.ToTable("TAB_Frequencia_Personalizada");

            builder.HasKey(f => f.Idf_Frequencia_Personalizada);

            builder.Property(f => f.Idf_Frequencia_Personalizada)
                .HasColumnName("Idf_Frequencia_Personalizada")
                .ValueGeneratedOnAdd();

            builder.Property(f => f.Idf_Frequencia)
                .HasColumnName("Idf_Frequencia")
                .IsRequired();

            builder.Property(f => f.Des_Frequencia_Personalizada)
                .HasColumnName("Des_Frequencia_Personalizada")
                .IsRequired();

            // Configure List<DateTime> as JSON in PostgreSQL with value comparer
            builder.Property(f => f.Dta_Frequencias)
                .HasColumnName("Dta_Frequencias")
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<DateTime>>(v, (JsonSerializerOptions?)null))
                .Metadata.SetValueComparer(new ValueComparer<List<DateTime>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

            builder.Property(f => f.Dta_Cadastro)
                .HasColumnName("Dta_Cadastro")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        }
    }
}