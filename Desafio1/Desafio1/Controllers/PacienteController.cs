using Desafio1.Data.NonPersistent;
using Desafio1.Models;
using System;
using System.Collections.Generic;

namespace Desafio1.Controllers
{
    public class PacienteController
    {
        // Referência a camada de dados
        private readonly Consultorio _consultorio;

        public PacienteController(Consultorio c)
        {
            _consultorio = c;
        }

        // Cria um Agendamento com os dados passados e Chama o respectivo método da camada de dados
        public void CadastrarPaciente(PacienteBuilder pb)
        {
            try
            {
                var paciente = pb.Build();
                _consultorio.AddPaciente(paciente);
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
                _consultorio.DeletePaciente(pb.Cpf);
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
            return _consultorio.GetAllPacientes();
        }
    }
}
