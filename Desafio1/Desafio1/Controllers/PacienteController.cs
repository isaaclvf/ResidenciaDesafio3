using Desafio1.Data.NonPersistent;
using Desafio1.Models;
using System;
using System.Collections.Generic;

namespace Desafio1.Controllers
{
    public class PacienteController
    {
        // Referência a camada de dados
        private readonly PacienteDao _pacienteDao = new();

        // Cria um Agendamento com os dados passados e Chama o respectivo método da camada de dados
        public void CadastrarPaciente(PacienteBuilder pb)
        {
            try
            {
                var paciente = pb.Build();
                _pacienteDao.Add(paciente);
            }
            // Escreve as Exceções não esperadas em stderr
            catch (Exception e) when (e is not Paciente.InvalidPacienteException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }
        // Recebe um AgendamentoBuilder e Chama o respectivo método da camada de Dados com as informações pertinentes
        public void ExcluirPaciente(PacienteBuilder pb)
        {
            try
            {
                _pacienteDao.Delete(pb.Cpf);
            }
            // Escreve as Exceções não esperadas em stderr
            catch (Exception e) when (e is not Paciente.InvalidPacienteException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }

        public IEnumerable<Paciente> GetPacientes()
        {
            return _pacienteDao.GetAll();
        }
    }
}
