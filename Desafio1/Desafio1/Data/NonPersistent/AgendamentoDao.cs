using Desafio1.Models;
using System.Collections.Generic;
using System;

namespace Desafio1.Data.NonPersistent
{
    public class AgendamentoDao
    {
        private readonly Data data = Data.GetInstance();
        public void Add(Agendamento a)
        {
            if(!data.CpfExists(a.CpfDoPaciente))
                throw new Agendamento.InvalidAgendamentoException("Cpf do Paciente informado não possui cadastro");

            if (data.IsAgendamentoCadastrado(a))
                throw new Agendamento.InvalidAgendamentoException("Horário já preenchido");

            var tmp = data.GetPacienteByCpf(a.CpfDoPaciente);
            try
            {
                tmp.AgendamentoFuturo = a;
            }catch(Exception)
            {
                throw new Agendamento.InvalidAgendamentoException("Paciente Informado já possui agendamento futuro");
            }
            a.Paciente = tmp;

            data.AddAgendamento(a);
        }

        public void Delete(ulong cpf, DateTime dataConsulta, ushort horaInicial)
        {
            if(!Agendamento.IsDateFuture(dataConsulta, horaInicial))
                throw new Agendamento.InvalidAgendamentoException("Agendamento não é data futura e, portanto, não pode ser cancelado");

            if (!data.DeleteAgendamento(cpf, dataConsulta, horaInicial))
                throw new Agendamento.InvalidAgendamentoException("Agendamento não cadastrado");

        }

        public IEnumerable<Agendamento> GetAll()
        {
            return data.GetAllAgendamentos();
        }
    }
}
