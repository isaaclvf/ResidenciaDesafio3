using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1.Models
{
    public static class CpfExtension
    {
        public static bool IsValidCpf(this string val)
        {
            return ValidaCpf(val);
        }

        private static int[] Seq(int x) => Enumerable.Range(2, x).Reverse().ToArray();
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
                        throw new Exception("Cpf do Cilente deve ter dígitos verficadores válidos");
                    }
                }
                else
                {
                    throw new Exception("Cpf do Cilente não pode ter todos os dígitos iguais");
                }
            }
            throw new Exception("Cpf do Cilente deve ter 11 dígitos");
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
    }
}
