using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booble_IA_API._3___Repository.Data.Mapper
{
    public class FrequenciaMap : IEntityTypeConfiguration<Frequencia>
    {
        public void Configure(EntityTypeBuilder<Frequencia> builder)
        {
            builder.ToTable("TAB_Frequencia");

            builder.HasKey(f => f.Idf_Frequencia);

            builder.Property(f => f.Idf_Frequencia)
                .HasColumnName("Idf_Frequencia")
                .ValueGeneratedOnAdd();

            builder.Property(f => f.Des_Frequencia)
                .HasColumnName("Des_Frequencia")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}