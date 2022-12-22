using Desafio1.Data.NonPersistent;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Desafio1.Models
{
    public class Agendamento
    {
        // Horários Limites do Consultório
        public const ushort Begin = 0830;
        public const ushort End = 1900;
        public ulong CpfDoPaciente { get; set; }
        public DateTime DataDaConsulta { get; set; }
        public ushort HoraInicial { get; set; }
        public ushort HoraFinal { get; set; }

        // Agendamento tem um Paciente
        public Paciente Paciente { get; set; }

        // Método estático que determina se uma data é futura ou não
        public static bool IsDateFuture(DateTime date, ushort hour)
        {
            var now = DateTime.Now;
            return (DateTime.Compare(date, now) > 0)
                || (date.Day == now.Day 
                    && (hour.Hora() > now.Hour 
                        || hour.Hora() == now.Hour && hour.Minuto() > now.Minute));
        }

        // Dois Agendamentos são iguais se possuem interseção de horário
        public override bool Equals(object obj)
        {
            return obj is Agendamento a && 
                ((this.HoraInicial <= a.HoraInicial && a.HoraInicial < this.HoraFinal)
                || (this.HoraInicial < a.HoraFinal && a.HoraFinal <= this.HoraFinal));
        }

        // Não se deve utilizar uma Estrutura Hash, apenas estruturas ordenadas
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

        // Deve-se implementar um comparador de Agendamentos para mantê-los ordenados por Data e Horario Inicial de forma crescente
        public class AgendamentoComparer : IComparer<Agendamento>
        {
            public int Compare(Agendamento x, Agendamento y)
            {
                if (x.DataDaConsulta == y.DataDaConsulta)
                {
                    if (x.Equals(y))
                        return 0;
                    else
                        return x.HoraInicial - y.HoraInicial;
                }

                return DateTime.Compare(x.DataDaConsulta, y.DataDaConsulta);
            }
        }
    }
}
