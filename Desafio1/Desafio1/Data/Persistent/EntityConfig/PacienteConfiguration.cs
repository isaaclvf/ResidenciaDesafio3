using Desafio1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio1.Data.Persistent.EntityConfig
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            // builder
            //     .Ignore(p => p.AgendamentoFuturo);

             builder
             .HasOne(p => p.AgendamentoFuturo);
             

            builder
                .Property(p => p.Nome)
                .IsRequired();

            builder
                .Property(p => p.Cpf)
                .IsRequired();

            builder
            .HasMany(p => p.Agendamentos)
            .WithOne(a => a.Paciente)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
