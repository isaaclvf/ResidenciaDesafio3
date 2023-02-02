using System.Collections.Generic;
using Desafio1.Models;

namespace Desafio1.Data.NonPersistent
{
    // Objeto Singleton que mantém um conjunto de Pacientes e um conjunto de Agendamentos
    public sealed class DefaultContext
    {
        // Conjunto de Pacientes
        internal HashSet<Paciente> Pacientes {get; set;}
        // Conjunto de Agendamentos Ordenados
        internal SortedSet<Agendamento> Agendamentos {get; set;}

        // Construtor Privado
        // Deve-se chamar o método GetInstance
        private DefaultContext() 
        { 
            Pacientes = new();
            Agendamentos = new(new Agendamento.AgendamentoComparer());
        }

        // Instancia única da classe, mantida em um campo de classe (static)
        private static DefaultContext _instance;

        // Método de classe que cria a instância do objeto, caso ela não exista, e a retorna
        public static DefaultContext GetInstance()
        {
            _instance ??= new DefaultContext();
            return _instance;
        }
    }
}