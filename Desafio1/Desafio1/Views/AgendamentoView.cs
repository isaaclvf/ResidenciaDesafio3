using Desafio1.Controllers;
using Desafio1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Desafio1.Views
{
    public class AgendamentoView
    {
        // Objeto para interagir com o usuário no "Agendamento de Consultas"
        private readonly AC agendarConsulta = new();

        // Objeto para interagir com o usuário no "Cancelamento de Consultas"
        private readonly CA cancelarAgendamento = new();

        // Objeto para interagir com o usuário no "Listagem de Agendamentos"
        private readonly LA getDatas = new();

        // Referência a camada de Controladores
        private readonly AgendamentoController ac = new();

        // Ler dados de Agendamento;
        // Tentar Agendar Consulta;
        // Relatar Erros ou Sucesso, dependendo do que ocorrer. 
        public void AgendarConsulta()
        {
            AgendamentoBuilder ab = new();
            try
            {
                agendarConsulta.Read(ab);
                ac.AgendarConsulta(ab);
                Console.WriteLine("\nSUCESSO:\tConsulta Agendada com Sucesso!\n");
            }
            catch (Agendamento.InvalidAgendamentoException e)
            {
                Console.WriteLine($"\nERRO:\t{e.Message}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("\nFATAL:\tErro inesperado ocorreu\n");
            }
        }

        // Ler entradas de Agendamento - Cancelamento: Cpf do Paciente, Data da Consulta, Horario Inicial;
        // Tentar Cancelar Consulta;
        // Relatar Erros ou Sucesso, dependendo do que ocorrer. 
        public void CancelarConsulta()
        {
            AgendamentoBuilder ab = new();

            try
            {
                cancelarAgendamento.Read(ab);
                ac.CancelarConsulta(ab);
                Console.WriteLine("\nSUCESSO:\tConsulta Cancelada com Sucesso!\n");
            }
            catch (Agendamento.InvalidAgendamentoException e)
            {
                Console.WriteLine($"\nERRO:\t{e.Message}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("\nFATAL:\tErro inesperado ocorreu\n");
            }
        }

        public void ListarAgendaInteira()
        {
            ImprimirLista(ac.GetAgendamentos());
        }

        // Ler entradas de Listagem de Agendamento Parcial: Data Incial, Data Final;
        // Imprimir Listagem de Agendamentos
        public void ListarAgendaParcial()
        {
            ListagemAgendamentoBuilder b = new();

            getDatas.Read(b);

            var tmp = ac.GetAgendamentos()
                .SkipWhile(x => x.DataDaConsulta < b.D1)
                .TakeWhile(x => x.DataDaConsulta <= b.D2);
            ImprimirLista(tmp);
        }

        // Imprimir Lista de Agendamentos, de acordo com o layout definido
        private static void ImprimirLista(IEnumerable<Agendamento> list)
        {
            var border = new string('-', 65);

            Console.WriteLine(border);
            Console.WriteLine($"{"Data",7} {"H.Ini",8} {"H.Fim",0} {"Tempo",0} {"Nome",-25} {"Dt.Nasc.",9}");
            Console.WriteLine(border);

            DateTime? date = null;
            foreach (var a in list)
            {
                bool printdate;
                if (a.DataDaConsulta.Equals(date))
                    printdate = false;
                else
                {
                    date = a.DataDaConsulta;
                    printdate = true;
                }

                Console.WriteLine($"{(printdate ? date?.ToString("d") : new string(' ', 10)),0} {a.HorarioInicial.String(),2} {a.HorarioFinal.String(),0} {a.HorarioInicial.TimeSpan(a.HorarioFinal)} {a.Paciente?.Nome,-25} {a.Paciente.DataDeNascimento:d}");
            }
            Console.WriteLine();
        }

        // Classe que implementa a leitura de dados no Agendamento de Consulta
        private class AC : Input<AgendamentoBuilder>
        {
            protected override string[] Messages
            {
                get => _messages;
            }

            protected override Action<AgendamentoBuilder, string>[] Actions
            {
                get => _actions;
            }


            private readonly string[] _messages =  {
                "CPF:",
                "Data da Consulta:",
                "Hora Inicial:",
                "Hora Final:"
            };

            private readonly Action<AgendamentoBuilder, string>[] _actions =
                {

                // Set CPF
                (cB, line) => { cB.SetCpf(line); },

                // Set Data da Consulta
                (cB, line) => { cB.SetDataDaConsulta(line); },

                // Set Hora Inicial
                (cB, line) => { cB.SetHoraInicial(line); },

                // Set Hora Final
                (cB, line) => { cB.SetHoraFinal(line); },

            };
        }

        // Classe que implementa a leitura de dados no Cancelamento de Consulta
        private class CA : Input<AgendamentoBuilder>
        {
            protected override string[] Messages
            {
                get => _messages;
            }

            protected override Action<AgendamentoBuilder, string>[] Actions
            {
                get => _actions;
            }

            protected string[] _messages =
                {
                "CPF:",
                "Data da Consulta:",
                "Hora Inicial:"
            };

            protected Action<AgendamentoBuilder, string>[] _actions = {
                // Set CPF
                (cB, line) => { cB.SetCpf(line); },

                 // Set Data da Consulta
                (cB, line) => { cB.SetDataDaConsulta(line); },

                // Set Hora Inicial
                (cB, line) => { cB.SetHoraInicial(line); }
            };
        }

        // Classe que implementa a leitura de dados na Listagem de Agendmentos
        private class LA : Input<ListagemAgendamentoBuilder>
        {

            protected override string[] Messages
            {
                get => _messages;
            }

            protected override Action<ListagemAgendamentoBuilder, string>[] Actions
            {
                get => _actions;
            }


            private readonly string[] _messages =  {
                "Data Inicial:",
                "Data Final:"
            };

            private readonly Action<ListagemAgendamentoBuilder, string>[] _actions =
                {

                // Set Data da Consulta
                (cB, line) => { cB.SetD1(line); },
                (cB, line) => { cB.SetD2(line); }

            };
        }

        // Classe que valida os dados de entrada na Listagem Parcial de Agendamentos
        private class ListagemAgendamentoBuilder
        {
            public DateTime D1 { get; set; }
            public DateTime D2 { get; set; }

            public void SetD1(string s)
            {
                D1 = ValidateDate(s);
            }

            public void SetD2(string s)
            {
                D2 = ValidateDate(s);
            }

            private static DateTime ValidateDate(string value)
            {

                if (DateTime.TryParseExact(s: value, format: "d", provider: new CultureInfo("pt-BR"), style: System.Globalization.DateTimeStyles.AllowWhiteSpaces, result: out DateTime dt))
                    return dt;
                throw new Exception("Formato de Data Inválida");
            }
        }
    }
}
