using Desafio1.Data.NonPersistent;
using Desafio1.Models;
using System;
using System.Collections.Generic;

namespace Desafio1.Controllers
{
    public class AgendamentoController
    {
        // Referência a camada de dados
        private readonly AgendamentoDao _agendamentoDao = new();

        public void AgendarConsulta(AgendamentoBuilder ab)
        {
            try
            {
                // Cria um Agendamento com os dados passados
                var agendamento = ab.Build();

                // Chama o respectivo método da camada de dados
                _agendamentoDao.Add(agendamento);
            }
            // Escreve as Exceções não esperadas em stderr
            catch (Exception e) when (e is not Agendamento.InvalidAgendamentoException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }

        // Recebe um AgendamentoBuilder e Chama o respectivo método da camada de Dados com as informações pertinentes
        public void CancelarConsulta(AgendamentoBuilder ab)
        {
            try
            {
                _agendamentoDao.Delete(ab.Cpf, ab.DataDaConsulta, ab.HorarioInicial);
            }
            // Escreve as Exceções não esperadas em stderr
            catch (Exception e) when (e is not Agendamento.InvalidAgendamentoException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }

        public IEnumerable<Agendamento> GetAgendamentos()
        {
            return _agendamentoDao.GetAll();
        }
    }
}
