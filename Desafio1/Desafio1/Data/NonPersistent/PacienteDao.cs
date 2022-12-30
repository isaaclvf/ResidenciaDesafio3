using Desafio1.Models;
using System;
using System.Collections.Generic;

namespace Desafio1.Data.NonPersistent
{
    // Paciente Data Access Object
    public class PacienteDao
    {
        // Referência ao contexto de Dados
        private readonly Data _data = Data.GetInstance();
        public void Add(Paciente p)
        {
            if (!_data.AddPaciente(p))
                throw new Paciente.InvalidPacienteException("Paciente já cadastrado");
        }

        public void Delete(ulong cpf)
        {
            var p = _data.GetPacienteByCpf(cpf);
            if(p is null)
                throw new Paciente.InvalidPacienteException("Paciente não cadastrado");

            if (p.AgendamentoFuturo is not null)
                throw new Paciente.InvalidPacienteException("Paciente possui agendamento futuro");

            _data.DeletePaciente(p);

            _data.DeleteAllAgendamentosFromPaciente(p);
        }

        public IEnumerable<Paciente> GetAll()
        {
            return _data.GetAllPacientes();
        }
    }
}
