using System;
using System.Collections.Generic;

namespace Desafio1.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public DateTime DataDeNascimento { get; set; }

        public List<Agendamento> Agendamentos { get; set; }

        public Paciente()
        {
            Agendamentos = new();
        }

        // Backing Field
        private Agendamento _agendamento;

        // Paciente pode ter zero ou um agendamento futuro
        public Agendamento AgendamentoFuturo
        {
            get
            {
                if (_agendamento == null) return null;
                // Verifica se atual agendamento futuro ainda é futuro (não passou)
                if (Agendamento.IsDateFuture(_agendamento.DataDaConsulta, _agendamento.HorarioInicial))
                    return _agendamento;
                
                // Se o agendamento não for mais futuro, o campo vira "null"
                _agendamento = null;
                return _agendamento;

            }

            set
            {
                if (_agendamento is null)
                {
                    _agendamento = value;
                }
                else
                {
                    // Verifica se atual agendamento futuro ainda é futuro (não passou)
                    if (Agendamento.IsDateFuture(_agendamento.DataDaConsulta, _agendamento.HorarioInicial))
                        throw new Exception("Cliente já possui agendamento futuro");
                    else
                        _agendamento = value;
                }
            }
        }

        // Dois Pacientes são iguais, se têm o mesmo Cpf
        public override bool Equals(object obj)
        {
            return obj is Paciente p && p.Cpf == this.Cpf;
        }


        // Como o Cpf é único entre Pacientes, o hashcode de um Paciente pode ser o hashcode de seu Cpf
        public override int GetHashCode()
        {
            return Cpf.GetHashCode();
        }

        public class InvalidPacienteException : Exception
        {
            public InvalidPacienteException(string message) : base(message) { }
        }
    }
}
