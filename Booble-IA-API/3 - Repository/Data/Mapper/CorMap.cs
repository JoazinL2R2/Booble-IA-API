using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booble_IA_API._3___Repository.Data.Mapper
{
    public class CorMap : IEntityTypeConfiguration<Cor>
    {
        public void Configure(EntityTypeBuilder<Cor> builder)
        {
            builder.ToTable("TAB_Cor");

            builder.HasKey(c => c.Idf_Cor);

            builder.Property(c => c.Idf_Cor)
                .HasColumnName("Idf_Cor")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Des_Cor)
                .HasColumnName("Des_Cor")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}