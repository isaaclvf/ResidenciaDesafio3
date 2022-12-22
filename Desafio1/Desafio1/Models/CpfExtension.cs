using System;
using System.Linq;

namespace Desafio1.Models
{
    // Cria métodos de Extensão sobre a classe ULong para a manipulação de CPFs
    public static class CpfExtension
    {
        public static bool IsValidCpf(this string val)
        {
            return ValidaCpf(val);
        }

        private static int[] Seq(int x) => Enumerable.Range(2, x).Reverse().ToArray();

        // Verifica se string tem formato de Cpf válido
        private static bool ValidaCpf(string s)
        {
            if (ulong.TryParse(s, out ulong _) && s.Length == 11)
            {
                if (s.Distinct().Count() != 1)
                {
                    if (IsDv(s, 9) && IsDv(s, 10))
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Cpf do Paciente deve ter dígitos verficadores válidos");
                    }
                }
                else
                {
                    throw new Exception("Cpf do Paciente não pode ter todos os dígitos iguais");
                }
            }
            throw new Exception("Cpf do Paciente deve ter 11 dígitos");
        }

        // Verifica se o (penúltimo ou último - parâmetro $lst) dígito é válido
        private static bool IsDv(string s, int lst)
        {
            var lstDigit = int.Parse($"{s[lst]}");
            var mod =
                s.Remove(lst).ToCharArray()
                .Zip(Seq(lst), (x, y) => int.Parse($"{x}") * y)
                .Aggregate(0, (acc, e) => acc + e)
                % 11;

            return
                mod switch
                {
                    0 or 1 => lstDigit == 0,
                    _ => lstDigit == 11 - mod,
                };
        }
        // Adiciona o zero antes do valor caso o cpf comece com zero
        public static string String(this ulong val)
        {
            var tmp = val.ToString();
            if (tmp.Length < 11)
                return $"{new String('0', 11 - tmp.Length)}{tmp}";
            return tmp;
        }
    }
}
