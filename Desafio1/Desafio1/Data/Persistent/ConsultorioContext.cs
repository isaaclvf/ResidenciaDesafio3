using Desafio1.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio1.Data.Persistent
{
    public class ConsultorioContext : DbContext
    {
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(@$"
                    Server=;
                    Database=;
                    User Id=;
                    Password=;
                    Port=")
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        }
    }
}
