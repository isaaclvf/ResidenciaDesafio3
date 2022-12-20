using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1.Views
{
    public abstract class Input<T>
    {
        /* 
        * As mensagens a serem impressas na interação com o usuário.
        * Cada mensagem representa uma propriedade da classe Cliente.
        */
        protected abstract string[] Messages { get; }

        protected abstract Action<T, string>[] Actions { get; }

        protected IEnumerable<(string, Action<T, string>)> Zipped
        {
            get => Messages.Zip(Actions);
        }

        public void Read(T builder)
        {
            foreach ((var message, var action) in Zipped)
            {

                do
                {
                    Console.WriteLine(message);
                    var line = Console.ReadLine();
                    try
                    {
                        action(builder, line);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                } while (true);
            }
        }
    }
}
