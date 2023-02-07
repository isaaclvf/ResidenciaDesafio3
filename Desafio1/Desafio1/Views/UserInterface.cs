using System;


namespace Desafio1.Views
{
    // Implementa a navegação por menus
    // Ao final de uma ação (e.g. Adicionar Paciente), retorna-se ao menu Anterior (e.g. Cadastro de Pacientes)
    // Entradas Inválidas mantém a aplicação no mesmo menu
    public class UserInterface
    {
        // Referências a camada de Visualização 
        private readonly  PacienteView pv = new();
        private readonly  AgendamentoView av = new();

        public char Menu()
        {
            Console.WriteLine("Menu Principal\n1-Cadastro de pacientes\n2-Agenda\n3-Fim");
            var tmp = GetInput();

            switch(tmp)
            {
                case '1':
                    return CadastroDePacientes();
                case '2':
                    return Agenda();
                case '3':
                    return '0';
                default:
                    Erro();
                    return Menu();
            }
        }

        public void Show(int cod, string mensagem="")
        {
            switch(cod) {
                case 0:
                    Console.WriteLine($"\nSUCESSO:\t{mensagem}\n");
                    break;
                case 1:
                    Console.WriteLine($"\nERRO:\t{mensagem}\n");
                    break;
                default:
                    Console.WriteLine("\nFATAL:\tErro inesperado ocorreu\n");
                    break;
            }
        }


        public  char CadastroDePacientes()
        {
            Console.WriteLine("Menu do Cadastro de Pacientes\n1-Cadastrar novo paciente\n2-Excluir paciente\n3-Listar pacientes(ordenado por CPF)\n4-Listar pacientes(ordenado por nome)\n5-Voltar p / menu principal");
            var tmp = GetInput();

            switch (tmp)
            {
                case '1' or '2' or '3' or '4':
                    return tmp;
                case '5':
                    return Menu();
                default:
                    Erro();
                    break;
            }
            return CadastroDePacientes();
        }

        public  char Agenda()
        {
            Console.WriteLine("Agenda\n1-Agendar consulta\n2-Cancelar agendamento\n3-Listar agenda\n4-Voltar p / menu principal");
            var tmp = GetInput();

            switch (tmp)
            {
                case '1':
                    return '5';
                case '2':
                    return '6';
                case '3':
                    return ListagemDeAgendas();
                case '4':
                    return Menu();
                default:
                    Erro();
                    break;
            }
            return Agenda();
        }

        private  char ListagemDeAgendas()
        {
            Console.WriteLine("Apresentar a agenda T-Toda ou P-Periodo: P");

            var tmp = GetInput();

            switch (tmp)
            {
                case 'T':
                    return '7';
                case 'P':
                    return '8';
                default:
                    Erro();
                    return ListagemDeAgendas();
            }
        }

        private  char GetInput()
        {
            var tmp = Console.ReadLine().Trim();

            if (tmp.Length != 1)
            {
                Erro();
                return GetInput();
            }
            else
                return tmp[0];
        }

        private  void Erro()
        {
            Console.WriteLine("Entrada Inválida. Digite Novamente");
        }
    }
}
