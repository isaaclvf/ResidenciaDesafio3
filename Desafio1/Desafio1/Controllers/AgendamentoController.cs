using Desafio1.Data.NonPersistent;
using Desafio1.Models;
using System;
using System.Collections.Generic;

namespace Desafio1.Controllers
{
    public class AgendamentoController
    {
        private readonly AgendamentoDao _idao = new();

        public void AgendarConsulta(AgendamentoBuilder ab)
        {
            try
            {
                var agendamento = ab.Build();
                _idao.Add(agendamento);
            }
            catch (Exception e) when (e is not Agendamento.InvalidAgendamentoException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }

        public void CancelarConsulta(AgendamentoBuilder ab)
        {
            try
            {
                _idao.Delete(ab.Cpf, ab.DataDaConsulta, ab.HoraInicial);
            }
            catch (Exception e) when (e is not Agendamento.InvalidAgendamentoException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }

        public IEnumerable<Agendamento> GetAgendamentos()
        {
            return _idao.GetAll();
        }
    }
}
