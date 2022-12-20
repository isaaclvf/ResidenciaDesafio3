using Desafio1.Data.NonPersistent;
using System;

namespace Desafio1.Models
{
    public class Agendamento
    {
        public const ushort Begin = 830;
        public const ushort End = 1900;
        public ulong CpfDoPaciente { get; set; }
        public DateTime DataDaConsulta { get; set; }
        public ushort HoraInicial { get; set; }
        public ushort HoraFinal { get; set; }

        public Paciente Paciente { get; set; }

        public static bool IsDateFuture(DateTime date, ushort hour)
        {
            var now = DateTime.Now;
            return (date > now)
                || (date.Day == now.Day && hour > now.Hour);
        }

        // Dois Agendamentos são iguais se possuem interseção de horário
        public override bool Equals(object obj)
        {
            return obj is Agendamento a && 
                ((this.HoraInicial <= a.HoraInicial && a.HoraInicial <= this.HoraFinal)
                || (this.HoraInicial <= a.HoraFinal && a.HoraFinal <= this.HoraFinal));
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }


        public class InvalidAgendamentoException : Exception
        {
            public string Campo { get; set; }
            public object Val { get; set; }
            public string Mensagem { get; set; }

            public InvalidAgendamentoException(string message) : base(message) { }

            public InvalidAgendamentoException(string fieldName, object val, string message)
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
