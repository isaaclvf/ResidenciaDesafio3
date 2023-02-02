using Desafio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio1.Data.NonPersistent
{
    public sealed class DefaultConsultorio : IConsultorio
    {
        DefaultContext _context = DefaultContext.GetInstance();

        // Métodos Públicos para a manipulação dos Dados (Agendamentos e Pacientes)

        public bool AddPaciente(Paciente p)
        {
            return _context.Pacientes.Add(p);
        }

        public bool DeletePaciente(Paciente p)
        {
            return _context.Pacientes.Remove(p);
        }

        public IEnumerable<Paciente> GetAllPacientes()
        {
            return _context.Pacientes.ToList();
        }
        public bool AddAgendamento(Agendamento a)
        {
            return _context.Agendamentos.Add(a);
        }

        public bool DeleteAgendamento(string cpf, DateTime date, ushort hour)
        {

            var tmp = _context.Agendamentos
                .FirstOrDefault(x => x.CpfDoPaciente == cpf && x.DataDaConsulta == date && x.HorarioInicial == hour);
            if (tmp is not null)
                return _context.Agendamentos.Remove(tmp);
            return false;
        }

        public IEnumerable<Agendamento> GetAllAgendamentos()
        {
            return _context.Agendamentos.ToList();
        }

        public bool CpfExists(string cpf)
        {
            return _context.Pacientes.Any(x => x.Cpf == cpf);
        }

        public Paciente GetPacienteByCpf(string cpf)
        {
            return _context.Pacientes.FirstOrDefault(x => x.Cpf == cpf);
        }

        public bool DeleteAllAgendamentosFromPaciente(Paciente p)
        {
            foreach (var a in _context.Agendamentos.Where(x => x.CpfDoPaciente == p.Cpf))
                _context.Agendamentos.Remove(a);

            return true;
        }

        public bool IsAgendamentoCadastrado(Agendamento a)
        {
            return _context.Agendamentos.Contains(a);
        }
    }
}
