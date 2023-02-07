using System.Collections.Generic;
using Desafio1.Models;

namespace Desafio1.Data
{
    public interface IPacienteDao
    {
        public bool AddPaciente(Paciente p);

        public bool DeletePaciente(Paciente p);

        public IEnumerable<Paciente> GetAllPacientes();

         public bool CpfExists(string cpf);

        public Paciente GetPacienteByCpf(string cpf);

        public bool UpdatePaciente(string cpf, Agendamento a);


    }
}