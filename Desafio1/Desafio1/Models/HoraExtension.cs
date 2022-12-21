using Desafio1.Data.NonPersistent;
using System;
using System.Linq;
using System.Text;

namespace Desafio1.Models
{
    public static class HoraExtension
    {
        public static bool IsValidHora(this string val)
        {
            if (val.Length != 4)
                throw new Exception("Hora deve ter quatro dígitos");

            var tmp = ushort.Parse(val);

            if (tmp > Agendamento.End || tmp < Agendamento.Begin)
                throw new Exception($"Hora deve ser de {Agendamento.Begin} até ${Agendamento.End}");

            return
                (tmp % 100) switch
                {
                    0 or 15 or 30 or 45 => true,
                    _ => throw new Exception("Hora deve ser de 15 em 15 minutos"),
                };

        }

        public static ushort Hora(this ushort val) => (ushort) (val / 100);

        public static ushort Minuto(this ushort val) => (ushort)(val % 100);

        public static string String(this ushort val) => $"{Format(val.Hora())}:{Format(val.Minuto())}";

        private static string Format(ushort hora) 
        {
            if (hora < 10)
                return $"0{hora}";
            else
            {
                return hora.ToString();
            }
        }

        public static string TimeSpan(this ushort e, ushort b)
        {
            var tmp = DateTime.ParseExact(b.String(), "t", null) 
                - DateTime.ParseExact(e.String(), "t", null);
            return $"{Format((ushort)tmp.Hours)}:{Format((ushort)tmp.Minutes)}";
        }

    }
}
