using System;
using System.Collections.Generic;
using Desafio1.Data;

namespace Desafio1.Models 
{
    public class Consultorio
    {
        private readonly IConsultorioDao _consultorio;

        public Consultorio(IConsultorioDao consultorio)
        {
            this._consultorio = consultorio;
        }

        public void AddAgendamento(Agendamento a)
        {
            if(!_consultorio.CpfExists(a.CpfDoPaciente))
                throw new Agendamento.InvalidAgendamentoException("Cpf do Paciente informado não possui cadastro");

            if (_consultorio.IsAgendamentoCadastrado(a))
                throw new Agendamento.InvalidAgendamentoException("Horário já preenchido");

            // Preencher campo Agendamento Futuro do Paciente
            _consultorio.UpdatePaciente(a.CpfDoPaciente, a);
        
        }

        public void DeleteAgendamento(string cpf, DateTime dataConsulta, ushort horaInicial)
        {
            if(!Agendamento.IsDateFuture(dataConsulta, horaInicial))
                throw new Agendamento.InvalidAgendamentoException("Agendamento não é data futura e, portanto, não pode ser cancelado");

            if (!_consultorio.DeleteAgendamento(cpf, dataConsulta, horaInicial))
                throw new Agendamento.InvalidAgendamentoException("Agendamento não cadastrado");

        }

        public IEnumerable<Agendamento> GetAllAgendamentos()
        {
            return _consultorio.GetAllAgendamentos();
        }

        public void AddPaciente(Paciente p)
        {
            if (!_consultorio.AddPaciente(p))
                throw new Paciente.InvalidPacienteException("Paciente já cadastrado");
        }

        public void DeletePaciente(string cpf)
        {
            var p = _consultorio.GetPacienteByCpf(cpf);
            if(p is null)
                throw new Paciente.InvalidPacienteException("Paciente não cadastrado");

            if (p.AgendamentoFuturo is not null)
                throw new Paciente.InvalidPacienteException("Paciente possui agendamento futuro");

            _consultorio.DeletePaciente(p);

            _consultorio.DeleteAllAgendamentosFromPaciente(p);
        }

        public IEnumerable<Paciente> GetAllPacientes()
        {
            return _consultorio.GetAllPacientes();
        }
    }
}