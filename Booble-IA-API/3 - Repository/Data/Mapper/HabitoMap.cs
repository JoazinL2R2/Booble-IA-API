using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property<string>("Dta_Conclusoes")
                .HasColumnName("Dta_Conclusoes")
                .HasMaxLength(2000);
        }
    }
}
