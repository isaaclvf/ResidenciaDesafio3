using Desafio1.Data.NonPersistent;
using Desafio1.Models;
using System;
using System.Collections.Generic;

namespace Desafio1.Controllers
{
    public class PacienteController
    {
        private readonly PacienteDao _idao = new();

        public void CadastrarPaciente(PacienteBuilder pb)
        {
            try
            {
                var paciente = pb.Build();
                _idao.Add(paciente);
            }
            catch (Exception e) when (e is not Paciente.InvalidPacienteException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }

        public void ExcluirPaciente(PacienteBuilder pb)
        {
            try
            {
                _idao.Delete(pb.Cpf);
            }
            catch (Exception e) when (e is not Paciente.InvalidPacienteException)
            {
                Console.Error.WriteLine($"{e.Message}\n{e}");
                throw;
            }
        }

        public IEnumerable<Paciente> GetPacientes()
        {
            return _idao.GetAll();
        }
    }
}
