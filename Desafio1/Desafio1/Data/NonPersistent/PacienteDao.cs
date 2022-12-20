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
                throw new Exception("Paciente já cadastrado");
        }

        public void Delete(ulong cpf)
        {
            var p = data.GetPacienteByCpf(cpf);
            if(p.AgendamentoFuturo is not null)
                throw new Exception("Paciente possui agendamento futuro");

            if (!data.DeletePaciente(p))
                throw new Exception("Paciente não cadastrado");

            data.DeleteAllAgendamentosFromPaciente(p);
        }

        public IEnumerable<Paciente> GetAll()
        {
            return data.GetAllPacientes();
        }
    }
}
