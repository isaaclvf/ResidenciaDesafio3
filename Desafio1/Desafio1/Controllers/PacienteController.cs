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
            catch (Exception)
            {
                throw new Paciente.InvalidPacienteException("Paciente já cadastrado");
            }
        }

        public void ExcluirPaciente(PacienteBuilder pb)
        {
            try
            {
                _idao.Delete(pb.Cpf);
            }
            catch (Exception)
            {
                throw new Paciente.InvalidPacienteException("Paciente não Cadastrado");
            }
        }

        public IEnumerable<Paciente> GetPacientes()
        {
            return _idao.GetAll();
        }
    }
}
