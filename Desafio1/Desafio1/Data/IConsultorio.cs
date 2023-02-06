using System;
using System.Collections.Generic;
using Desafio1.Models;

namespace Desafio1.Data 
{
    public interface IConsultorio 
    {    
        // Métodos Públicos para a manipulação dos Dados (Agendamentos e Pacientes)

        public bool AddPaciente(Paciente p);

        public bool DeletePaciente(Paciente p);

        public IEnumerable<Paciente> GetAllPacientes();
        public bool AddAgendamento(Agendamento a);

        public bool DeleteAgendamento(string cpf, DateTime date, ushort hour);

        public IEnumerable<Agendamento> GetAllAgendamentos();

        public bool CpfExists(string cpf);

        public Paciente GetPacienteByCpf(string cpf);

        public bool DeleteAllAgendamentosFromPaciente(Paciente p);

        public bool IsAgendamentoCadastrado(Agendamento a);
        public bool UpdatePaciente(string cpf, Agendamento a);
    }
}