using Desafio1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio1.Data.Persistent.EntityConfig
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder
                .Property(a => a.CpfDoPaciente)
                .IsRequired();
        }
    }
}
