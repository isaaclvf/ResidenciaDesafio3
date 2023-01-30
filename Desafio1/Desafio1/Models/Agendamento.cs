using System;
using System.Collections.Generic;

namespace Desafio1.Models
{
    public class Agendamento
    {
        // Horários Limites do Consultório
        public const ushort Begin = 0830;
        public const ushort End = 1900;
        public ulong CpfDoPaciente { get; set; }
        public DateTime DataDaConsulta { get; set; }

        // Horários são representadas por inteiros e existem métodos de extensão em HoraExtension para as manipular
        public ushort HorarioInicial { get; set; }
        public ushort HorarioFinal { get; set; }

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
        // Facilita na hora de adicionar um agendamento a um conjunto
        public override bool Equals(object obj)
        {
            return obj is Agendamento a && 
                ((this.HorarioInicial <= a.HorarioInicial && a.HorarioInicial < this.HorarioFinal)
                || (this.HorarioInicial < a.HorarioFinal && a.HorarioFinal <= this.HorarioFinal));
        }

        // Não se deve utilizar uma Estrutura Hash, apenas estruturas ordenadas
        public override int GetHashCode()
        {
            return HashCode.Combine(this.CpfDoPaciente, this.DataDaConsulta, this.HorarioInicial);
        }

        public class InvalidAgendamentoException : Exception
        {
            public InvalidAgendamentoException(string message) : base(message) { }
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
                        return x.HorarioInicial - y.HorarioInicial;
                }

                return DateTime.Compare(x.DataDaConsulta, y.DataDaConsulta);
            }
        }
    }
}
