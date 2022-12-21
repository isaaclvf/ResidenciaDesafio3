using Desafio1.Data.NonPersistent;
using System;
using System.Linq;
using System.Text;

namespace Desafio1.Models
{
    public static class HoraExtension
    {
        // Valida se uma string tem o formato de hora válido
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
        // Pegar apenas a hora
        public static ushort Hora(this ushort val) => (ushort) (val / 100);
        // Pegar apenas os minutos
        public static ushort Minuto(this ushort val) => (ushort)(val % 100);
        // Converter hora para String
        public static string String(this ushort val) => $"{Format(val.Hora())}:{Format(val.Minuto())}";
       
        // Formatar hora do jeito apropriado
        private static string Format(ushort hora) 
        {
            if (hora < 10)
                return $"0{hora}";
            else
            {
                return hora.ToString();
            }
        }
        // Subtrair duas horas e pegar o tempo
        public static string TimeSpan(this ushort e, ushort b)
        {
            var tmp = DateTime.ParseExact(b.String(), "t", null) 
                - DateTime.ParseExact(e.String(), "t", null);
            return $"{Format((ushort)tmp.Hours)}:{Format((ushort)tmp.Minutes)}";
        }

    }
}
