using Desafio1.Models;
using System.Collections.Generic;
using System;

namespace Desafio1.Data.NonPersistent
{
    // Agendamento Data Access Object
    public class AgendamentoDao
    {
        // Referência ao contexto de Dados
        private readonly Data _data = Data.GetInstance();

        public void Add(Agendamento a)
        {
            if(!_data.CpfExists(a.CpfDoPaciente))
                throw new Agendamento.InvalidAgendamentoException("Cpf do Paciente informado não possui cadastro");

            if (_data.IsAgendamentoCadastrado(a))
                throw new Agendamento.InvalidAgendamentoException("Horário já preenchido");

            // Preencher campo Agendamento Futuro do Paciente
            var tmp = _data.GetPacienteByCpf(a.CpfDoPaciente);
            try
            {
                tmp.AgendamentoFuturo = a;
            }catch(Exception)
            {
                throw new Agendamento.InvalidAgendamentoException("Paciente Informado já possui agendamento futuro");
            }
            // Preencher campo Paciente do Agendamento
            a.Paciente = tmp;

            // Adicionar Agendamento ao contexto de Dados
            _data.AddAgendamento(a);
        }

        public void Delete(ulong cpf, DateTime dataConsulta, ushort horaInicial)
        {
            if(!Agendamento.IsDateFuture(dataConsulta, horaInicial))
                throw new Agendamento.InvalidAgendamentoException("Agendamento não é data futura e, portanto, não pode ser cancelado");

            if (!_data.DeleteAgendamento(cpf, dataConsulta, horaInicial))
                throw new Agendamento.InvalidAgendamentoException("Agendamento não cadastrado");

        }

        public IEnumerable<Agendamento> GetAll()
        {
            return _data.GetAllAgendamentos();
        }
    }
}
