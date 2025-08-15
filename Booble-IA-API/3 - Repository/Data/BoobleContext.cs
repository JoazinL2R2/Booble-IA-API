using System;
using Booble_IA_API._3___Repository.Data.Mapper;
using Booble_IA_API._3___Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Booble_IA_API._3___Repository.Data
{
    public class BoobleContext : DbContext
    {
        public BoobleContext(DbContextOptions<BoobleContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Amizade> Amizades { get; set; }
        public DbSet<Habito> Habitos { get; set; }
        public DbSet<RankingStreakDTO> RankingStreaks { get; set; }
        public DbSet<Cor> Cores { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<FrequenciaPersonalizada> FrequenciasPersonalizadas { get; set; }
        public DbSet<HabitoIcone> HabitoIcones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RankingStreakDTO>().HasNoKey();
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new AmizadeMap());
            modelBuilder.ApplyConfiguration(new HabitoMap());
            modelBuilder.ApplyConfiguration(new CorMap());
            modelBuilder.ApplyConfiguration(new FrequenciaMap());
            modelBuilder.ApplyConfiguration(new FrequenciaPersonalizadaMap());
            modelBuilder.ApplyConfiguration(new HabitoIconeMap());
        }
    }
}
