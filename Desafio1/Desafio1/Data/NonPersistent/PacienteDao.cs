using Desafio1.Models;
using System;
using System.Collections.Generic;

namespace Desafio1.Data.NonPersistent
{
    public class PacienteDao
    {
        private readonly Data data = Data.GetInstance();
        public void Add(Paciente p)
        {
            if (!data.AddPaciente(p))
                throw new Paciente.InvalidPacienteException("Paciente já cadastrado");
        }

        public void Delete(ulong cpf)
        {
            var p = data.GetPacienteByCpf(cpf);
            if(p is null)
                throw new Paciente.InvalidPacienteException("Paciente não cadastrado");

            if (p.AgendamentoFuturo is not null)
                throw new Paciente.InvalidPacienteException("Paciente possui agendamento futuro");

            data.DeletePaciente(p);

            data.DeleteAllAgendamentosFromPaciente(p);
        }

        public IEnumerable<Paciente> GetAll()
        {
            return data.GetAllPacientes();
        }
    }
}
