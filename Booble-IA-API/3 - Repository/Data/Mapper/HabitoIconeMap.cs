using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booble_IA_API._3___Repository.Data.Mapper
{
    public class HabitoIconeMap : IEntityTypeConfiguration<HabitoIcone>
    {
        public void Configure(EntityTypeBuilder<HabitoIcone> builder)
        {
            builder.ToTable("TAB_Habito_Icone");

            builder.HasKey(h => h.Idf_Habito_Icone);

            builder.Property(h => h.Idf_Habito_Icone)
                .HasColumnName("Idf_Habito_Icone")
                .ValueGeneratedOnAdd();

            builder.Property(h => h.Des_Habito_Icone)
                .HasColumnName("Des_Habito_Icone")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}