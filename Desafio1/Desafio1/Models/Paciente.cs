using System;

namespace Desafio1.Models
{
    public class Paciente
    {
        public string Nome { get; set; }
        public ulong Cpf { get; set; }

        public DateTime DataDeNascimento { get; set; }

        // Backing Field
        private Agendamento _agendamento;

        // Paciente pode ter zero ou um agendamento futuro
        public Agendamento AgendamentoFuturo
        {
            get
            {
                if (_agendamento == null) return null;
                if (Agendamento.IsDateFuture(_agendamento.DataDaConsulta, _agendamento.HoraInicial))
                    return _agendamento;

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
                    if (Agendamento.IsDateFuture(_agendamento.DataDaConsulta, _agendamento.HoraInicial))
                        throw new Exception("Cliente já possui agendamento futuro");
                    else
                        _agendamento = value;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Paciente p && p.Cpf == this.Cpf;
        }

        public override int GetHashCode()
        {
            return Cpf.GetHashCode();
        }

        public class InvalidPacienteException : Exception
        {
            public string Campo { get; set; }
            public object Val { get; set; }
            public string Mensagem { get; set; }

            public InvalidPacienteException(string message) : base(message) { }

            public InvalidPacienteException(string fieldName, object val, string message)
                : base(MakeMessage(fieldName, val, message))
            {
                Campo = fieldName;
                Val = val;
                Mensagem = message;

            }

            internal static string MakeMessage(string fieldName, object val, string message)
            {
                return $"Campo: {fieldName} Valor: {(val.ToString().Length == 0 ? "N/A" : val),5} Mensagem: {message}";
            }
        }
    }
}
