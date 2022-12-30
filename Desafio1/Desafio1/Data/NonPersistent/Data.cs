using Desafio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1.Data.NonPersistent
{
    // Objeto Singleton que mantém um conjunto de Pacientes e um conjunto de Agendamentos
    public sealed class Data
    {
        // Conjunto de Pacientes
        private readonly HashSet<Paciente> pacientes = new();
        // Conjunto de Agendamentos Ordenados
        private readonly SortedSet<Agendamento> agendamentos = new(new Agendamento.AgendamentoComparer());

        // Construtor Privado
        // Deve-se chamar o método GetInstance
        private Data() { }

        // Instancia única da classe, mantida em um campo de classe (static)
        private static Data _instance;

        // Método de classe que cria a instância do objeto, caso ela não exista, e a retorna
        public static Data GetInstance()
        {
            _instance ??= new Data();
            return _instance;
        }

        // Métodos Públicos para a manipulação dos Dados (Agendamentos e Pacientes)

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
                .FirstOrDefault(x => x.CpfDoPaciente == cpf && x.DataDaConsulta == date && x.HorarioInicial == hour);
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
