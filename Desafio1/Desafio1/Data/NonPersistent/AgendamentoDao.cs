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
                throw new Agendamento.InvalidAgendamentoException("Cpf do Paciente", a.CpfDoPaciente, "Valor informado não possui cadastro");

            if (!data.AddAgendamento(a))
                throw new Exception("Agendamento já cadastrado");

            var tmp = data.GetPacienteByCpf(a.CpfDoPaciente);
            tmp.AgendamentoFuturo = a;
            a.Paciente = tmp;
        }

        public void Delete(ulong cpf, DateTime dataConsulta, ushort horaInicial)
        {
            if(!Agendamento.IsDateFuture(dataConsulta, horaInicial))
                throw new Exception("Agendamento não é data futura e, portanto, não pode ser cancelado");

            if (!data.DeleteAgendamento(cpf, dataConsulta, horaInicial))
                throw new Exception("Agendamento não cadastrado");

        }

        public IEnumerable<Agendamento> GetAll()
        {
            return data.GetAllAgendamentos();
        }
    }
}
