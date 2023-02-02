using System;
using Desafio1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Desafio1.Data.Entity
{

    public class EntityContext : DbContext
    {
        internal DbSet<Agendamento> Agendamentos {get; set;}
        internal DbSet<Paciente> Pacientes {get; set;}

        public EntityContext() {}

         public EntityContext(DbContextOptions<EntityContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            // Paciente Chave Primária
            modelBuilder.Entity<Paciente>()
            .HasKey(p => p.Cpf);
            
            // Nome não nullable
            modelBuilder.Entity<Paciente>()
            .Property(p => p.Nome)
            .IsRequired();

            // Agendamento Chave Primária
            modelBuilder.Entity<Agendamento>()
            .HasKey(a => new { a.CpfDoPaciente, a.DataDaConsulta, a.HorarioInicial});

            // Foreing Key
            modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Paciente)
            .WithOne(p => p.AgendamentoFuturo)
            .HasForeignKey<Agendamento>(a => a.CpfDoPaciente);
        }

        protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseNpgsql(PostgresConsultorioContext.Connection());
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