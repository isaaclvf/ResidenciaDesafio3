using Desafio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1.Data.NonPersistent
{
    public sealed class Data
    {

        private readonly HashSet<Paciente> pacientes = new();
        private readonly SortedSet<Agendamento> agendamentos = new(new Agendamento.AgendamentoComparer());

        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private Data() { }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static Data _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static Data GetInstance()
        {
            _instance ??= new Data();
            return _instance;
        }

        public bool AddPaciente(Paciente p)
        {
            return pacientes.Add(p);
        }

        public bool DeletePaciente(Paciente p)
        {
            return pacientes.Remove(p);
        }

        public IEnumerable<Paciente> GetAllPacientes()
        {
            return pacientes.ToList();
        }
        public bool AddAgendamento(Agendamento a)
        {
            return agendamentos.Add(a);
        }

        public bool DeleteAgendamento(ulong cpf, DateTime date, ushort hour)
        {

            var tmp = agendamentos
                .FirstOrDefault(x => x.CpfDoPaciente == cpf && x.DataDaConsulta == date && x.HoraInicial == hour);
            if (tmp is not null)
                return agendamentos.Remove(tmp);
            return false;
        }

        public IEnumerable<Agendamento> GetAllAgendamentos()
        {
            return agendamentos.ToList();
        }

        public bool CpfExists(ulong cpf)
        {
            return pacientes.Any(x => x.Cpf == cpf);
        }

        public Paciente GetPacienteByCpf(ulong cpf)
        {
            return pacientes.FirstOrDefault(x => x.Cpf == cpf);
        }

        public bool DeleteAllAgendamentosFromPaciente(Paciente p)
        {
            foreach (var a in agendamentos.Where(x => x.CpfDoPaciente == p.Cpf))
                agendamentos.Remove(a);

            return true;
        }

        public bool IsAgendamentoCadastrado(Agendamento a)
        {
            return agendamentos.Contains(a);
        }
    }
}
