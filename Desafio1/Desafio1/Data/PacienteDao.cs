using Desafio1.Data.Persistent;
using Desafio1.Data.Persistent.DbConfig;
using Desafio1.Models;
using System.Collections.Generic;

namespace Desafio1.Data.NonPersistent
{
    // Paciente Data Access Object
    public class PacienteDao
    {
        // Referência ao contexto de Dados
        private readonly IConsultorio _consultorio;

        public PacienteDao(IConsultorio consultorio) {
            _consultorio = consultorio;
        }
        public PacienteDao()
        {
            // _consultorio = new DefaultConsultorio();
            _consultorio = new EntityConsultorio(new ConsultorioContextFactory().CreateDbContext());
            // _consultorio = new EntityConsultorio(new EntityContext());
        }

        public void Add(Paciente p)
        {
            if (!_consultorio.AddPaciente(p))
                throw new Paciente.InvalidPacienteException("Paciente já cadastrado");
        }

        public void Delete(string cpf)
        {
            var p = _consultorio.GetPacienteByCpf(cpf);
            if(p is null)
                throw new Paciente.InvalidPacienteException("Paciente não cadastrado");

            if (p.AgendamentoFuturo is not null)
                throw new Paciente.InvalidPacienteException("Paciente possui agendamento futuro");

            _consultorio.DeletePaciente(p);

            _consultorio.DeleteAllAgendamentosFromPaciente(p);
        }

        public IEnumerable<Paciente> GetAll()
        {
            return _consultorio.GetAllPacientes();
        }
    }
}
