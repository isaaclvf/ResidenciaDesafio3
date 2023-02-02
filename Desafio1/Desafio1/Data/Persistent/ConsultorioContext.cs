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
            // TODO: .env
            optionsBuilder
                .UseNpgsql(@$"
                    Server=babar.db.elephantsql.com;
                    Database=rodhrtvh;
                    User Id=rodhrtvh;
                    Password=TmApvZ5D_Dw4SwU5lCwJUPy77yEfvbyF;
                    Port=5432")
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        }
    }
}
