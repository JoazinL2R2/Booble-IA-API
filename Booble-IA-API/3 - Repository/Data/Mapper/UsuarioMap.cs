using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booble_IA_API._3___Repository.Data.Mapper
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TAB_Usuario");

            builder.HasKey(u => u.Idf_Usuario);

            builder.Property(u => u.Idf_Usuario)
                .HasColumnName("Idf_Usuario")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Des_Email)
                .HasColumnName("Des_Email")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Des_Nme)
                .HasColumnName("Des_Nme")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Num_Telefone)
                .HasColumnName("Num_Telefone")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(u => u.Senha)
                .HasColumnName("Senha")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Flg_Sexo)
                .HasColumnName("Flg_Sexo")
                .IsRequired();

            builder.Property(u => u.Dta_Cadastro)
                .HasColumnName("Dta_Cadastro")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(u => u.Dta_Alteracao)
                .HasColumnName("Dta_Alteracao")
                .IsRequired();
        }
    }
}
