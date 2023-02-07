using Desafio1.Data.Persistent.EntityConfig;
using Desafio1.Data.Persistent.DbConfig;
using Desafio1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Desafio1.Data.Persistent
{
    public class ConsultorioContext : DbContext
    {
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        public ConsultorioContext(DbContextOptions<ConsultorioContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        }

        protected sealed override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion(typeof(DateTimeToDateTimeUtc));
        }

        public class DateTimeToDateTimeUtc : ValueConverter<DateTime, DateTime>
        {
            public DateTimeToDateTimeUtc() : base(c => DateTime.SpecifyKind(c, DateTimeKind.Utc), c => c)
            {

            }
        }
    }
}
