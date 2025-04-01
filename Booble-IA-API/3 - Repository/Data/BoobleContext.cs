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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new AmizadeMap());
        }
    }
}
