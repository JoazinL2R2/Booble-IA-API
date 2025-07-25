﻿// <auto-generated />
using System;
using Booble_IA_API._3___Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Booble_IA_API.Migrations
{
    [DbContext(typeof(BoobleContext))]
    partial class BoobleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Booble_IA_API._3___Repository.Entities.Amizade", b =>
                {
                    b.Property<int>("Idf_Amizade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Idf_Amizade");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Idf_Amizade"));

                    b.Property<DateTime>("Dta_Cadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Dta_Cadastro")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("Flg_Aceito")
                        .HasColumnType("boolean")
                        .HasColumnName("Flg_Aceito");

                    b.Property<bool>("Flg_Inativo")
                        .HasColumnType("boolean")
                        .HasColumnName("Flg_Inativo");

                    b.Property<int>("Idf_Usuario_Recebedor")
                        .HasColumnType("integer")
                        .HasColumnName("Idf_Usuario_Recebedor");

                    b.Property<int>("Idf_Usuario_Solicitante")
                        .HasColumnType("integer")
                        .HasColumnName("Idf_Usuario_Solicitante");

                    b.HasKey("Idf_Amizade");

                    b.ToTable("TAB_Amizade", (string)null);
                });

            modelBuilder.Entity("Booble_IA_API._3___Repository.Entities.Habito", b =>
                {
                    b.Property<int>("Idf_Habito")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Idf_Habito");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Idf_Habito"));

                    b.Property<string>("Des_Cor")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Des_Cor");

                    b.Property<string>("Des_Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("Des_Descricao");

                    b.Property<string>("Des_Habito")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Des_Habito");

                    b.Property<string>("Des_Icone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Des_Icone");

                    b.Property<string>("Des_Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Des_Titulo");

                    b.Property<DateTime>("Dta_Cadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Dta_Cadastro")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Dta_Conclusoes")
                        .HasColumnType("jsonb")
                        .HasColumnName("Dta_Conclusoes");

                    b.Property<bool>("Flg_Concluido")
                        .HasColumnType("boolean")
                        .HasColumnName("Flg_Concluido");

                    b.Property<bool?>("Flg_Timer")
                        .HasColumnType("boolean")
                        .HasColumnName("Flg_Timer");

                    b.Property<int>("Idf_Frequencia")
                        .HasColumnType("integer")
                        .HasColumnName("Idf_Frequencia");

                    b.Property<int>("Num_Xp")
                        .HasColumnType("integer")
                        .HasColumnName("Num_Xp");

                    b.Property<decimal?>("Timer_Duracao")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("Timer_Duracao");

                    b.HasKey("Idf_Habito");

                    b.ToTable("TAB_Habito", (string)null);
                });

            modelBuilder.Entity("Booble_IA_API._3___Repository.Entities.Usuario", b =>
                {
                    b.Property<int>("Idf_Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Idf_Usuario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Idf_Usuario"));

                    b.Property<string>("Des_Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Des_Email");

                    b.Property<string>("Des_Nme")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Des_Nme");

                    b.Property<DateTime>("Dta_Alteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Dta_Alteracao");

                    b.Property<DateTime>("Dta_Cadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Dta_Cadastro")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("Dta_Nascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Flg_Sexo")
                        .HasColumnType("boolean")
                        .HasColumnName("Flg_Sexo");

                    b.Property<string>("Num_Telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("Num_Telefone");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Senha");

                    b.HasKey("Idf_Usuario");

                    b.ToTable("TAB_Usuario", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
