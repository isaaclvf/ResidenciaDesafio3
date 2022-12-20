using System;


namespace Desafio1.Views
{
    public class UserInterface
    {
        private readonly static PacienteView pv = new();
        private readonly static AgendamentoView av = new();

        public static void Start()
        {
            Menu();
        }

        private static void Menu()
        {
            Console.WriteLine("Menu Principal\n1-Cadastro de pacientes\n2-Agenda\n3-Fim");
            var tmp = GetInput();

            switch(tmp)
            {
                case '1':
                    CadastroDePacientes();
                    break;
                case '2':
                    Agenda();
                    break;
                case '3':
                    break;
                default:
                    Erro();
                    Menu();
                    break;
            }
        }

        private static void CadastroDePacientes()
        {
            Console.WriteLine("Menu do Cadastro de Pacientes\n1-Cadastrar novo paciente\n2-Excluir paciente\n3-Listar pacientes(ordenado por CPF)\n4-Listar pacientes(ordenado por nome)\n5-Voltar p / menu principal");
            var tmp = GetInput();

            switch (tmp)
            {
                case '1':
                    pv.CadastrarPaciente();
                    break;
                case '2':
                    pv.ExcluirPaciente();
                    break;
                case '3':
                    pv.ListarPacientesPorCpf();
                    break;
                case '4':
                    pv.ListarPacientesPorNome();
                    break;
                case '5':
                    Menu();
                    return;
                default:
                    Erro();
                    break;
            }
            CadastroDePacientes();
        }

        private static void Agenda()
        {
            Console.WriteLine("Agenda\n1-Agendar consulta\n2-Cancelar agendamento\n3-Listar agenda\n4-Voltar p / menu principal");
            var tmp = GetInput();

            switch (tmp)
            {
                case '1':
                    av.AgendarConsulta();
                    break;
                case '2':
                    av.CancelarConsulta();
                    break;
                case '3':
                    ListagemDeAgendas();
                    break;
                case '4':
                    Menu();
                    return;
                default:
                    Erro();
                    break;
            }
            Agenda();
        }

        private static void ListagemDeAgendas()
        {
            Console.WriteLine("Apresentar a agenda T-Toda ou P-Periodo: P"); //Data inicial: 01 / 01 / 2022\nData final: 05 / 01 / 2022");

            var tmp = GetInput();

            switch (tmp)
            {
                case 'T':
                    av.ListarAgendaInteira();
                    break;
                case 'P':
                    av.ListarAgendaParcial();
                    break;
                default:
                    Erro();
                    ListagemDeAgendas();
                    break;
            }
            Agenda();
        }

        private static char GetInput()
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

        private static void Erro()
        {
            Console.WriteLine("Entrada Inválida. Digite Novamente");
        }
    }
}
