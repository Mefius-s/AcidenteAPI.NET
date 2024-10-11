using Fiap.Api.Acidentes.Models;
using Microsoft.EntityFrameworkCore;
namespace Fiap.Api.Acidentes.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AcidenteModel> Acidentes { get; set; } // Add the AcidenteModel DbSet

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuration for AcidenteModel
            modelBuilder.Entity<AcidenteModel>()
                .ToTable("tb_acidentes")
                .HasKey(a => a.Id);
            modelBuilder.Entity<AcidenteModel>()
                .Property(a => a.DataAcidente)
                .IsRequired()
                .HasColumnName("data_acidente");
            modelBuilder.Entity<AcidenteModel>()
                .Property(a => a.HoraAcidente)
                .IsRequired()
                .HasColumnName("hora_acidente");
            modelBuilder.Entity<AcidenteModel>()
                .Property(a => a.Gravidade)
                .HasColumnName("gravidade");
            modelBuilder.Entity<AcidenteModel>()
                .Property(a => a.Endereco)
                .HasColumnName("endereco");

        }
    }
}