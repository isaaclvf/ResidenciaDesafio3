using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio1.Views
{
    // Template Method
    // Classe Abstrata com um único método não abstrato "Read" que modela como a entrada de Dados deve ser feita 
    public abstract class Input<T>
    {
        /* 
        * As mensagens a serem impressas na interação com o usuário.
        * Cada mensagem representa uma propriedade da classe Cliente.
        */
        protected abstract string[] Messages { get; }

        // A ações a serem feitas para cada entrada. E,g. SetNome.
        protected abstract Action<T, string>[] Actions { get; }


        protected IEnumerable<(string, Action<T, string>)> Zipped
        {
            get => Messages.Zip(Actions);
        }
        
        public void Read(T builder)
        {
            // Para cada mensgem e ação em Messages e Actions
            foreach ((var message, var action) in Zipped)
            {

                do
                {
                    // Escrever Mensagem
                    Console.WriteLine(message);
                    // Ler entrada
                    var line = Console.ReadLine();
                    try
                    {
                        // Executar Ação
                        action(builder, line);
                        // Se ação for sucedida, ir para próximo campo
                        // Isto é, sair do loop While
                        break;
                    }
                    // Se ação falhar, escrever mensagem de Erro e ler Campo de novo (i.e. outra iteração do loop while)
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                } while (true);
            }
        }
    }
}
