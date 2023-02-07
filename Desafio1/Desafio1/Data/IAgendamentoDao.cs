using System;
using System.Collections.Generic;
using Desafio1.Models;

namespace Desafio1.Data
{
    public interface IAgendamentoDao
    {
         public bool AddAgendamento(Agendamento a);

        public bool DeleteAgendamento(string cpf, DateTime date, ushort hour);

        public IEnumerable<Agendamento> GetAllAgendamentos();

        public bool DeleteAllAgendamentosFromPaciente(Paciente p);

        public bool IsAgendamentoCadastrado(Agendamento a);
    }
}