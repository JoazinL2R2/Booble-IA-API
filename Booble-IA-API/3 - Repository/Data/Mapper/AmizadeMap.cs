using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booble_IA_API._3___Repository.Data.Mapper
{
    public class AmizadeMap : IEntityTypeConfiguration<Amizade>
    {
        public void Configure(EntityTypeBuilder<Amizade> builder)
        {
            builder.ToTable("TAB_Amizade");

            builder.HasKey(u => u.Idf_Amizade);

            builder.Property(u => u.Idf_Amizade)
                .HasColumnName("Idf_Amizade")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Idf_Usuario_Solicitante)
                .HasColumnName("Idf_Usuario_Solicitante")
                .IsRequired();            
            
            builder.Property(u => u.Idf_Usuario_Recebedor)
                .HasColumnName("Idf_Usuario_Recebedor")
                .IsRequired();

            builder.Property(u => u.Flg_Inativo)
                .HasColumnName("Flg_Inativo")
                .IsRequired();            
            
            builder.Property(u => u.Flg_Aceito)
                .HasColumnName("Flg_Aceito")
                .IsRequired();

            builder.Property(u => u.Dta_Cadastro)
                .HasColumnName("Dta_Cadastro")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        }
    }
}
