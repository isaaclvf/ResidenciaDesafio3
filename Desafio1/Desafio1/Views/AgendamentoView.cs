using Desafio1.Controllers;
using Desafio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio1.Views
{
    public class AgendamentoView
    {
        private readonly AC agendarConsulta = new();
        private readonly CA cancelarAgendamento = new();

        private readonly LA1 getDataInicial = new();
        private readonly LA2 getDataFinal = new();

        private readonly AgendamentoController ac = new();
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

        public void ListarAgendaParcial()
        {
            AgendamentoBuilder b1 = new();
            AgendamentoBuilder b2 = new();

            getDataInicial.Read(b1);

            getDataFinal.Read(b2);

            var tmp = ac.GetAgendamentos()
                .SkipWhile(x => x.DataDaConsulta < b1.DataDaConsulta)
                .TakeWhile(x => x.DataDaConsulta >= b2.DataDaConsulta);
            ImprimirLista(tmp);
        }

        private static void ImprimirLista(IEnumerable<Agendamento> list)
        {
            var border = new string('-', 65);

            Console.WriteLine(border);
            Console.WriteLine($"{"Data",7} {"H.Ini",8} {"H.Fim",0} {"Tempo",0} {"Nome",-25} {"Dt.Nasc.",9}");
            Console.WriteLine(border);

            DateTime? date = null;
            var printdate = true;
            foreach (var a in list)
            {
                if (a.DataDaConsulta.Equals(date))
                    printdate = false;
                else
                    date = a.DataDaConsulta;

                Console.WriteLine($"{(printdate ? date?.ToString("d") : new string(' ', 10)),0} {a.HoraInicial.String(),2} {a.HoraFinal.String(),0} {a.HoraInicial.TimeSpan(a.HoraFinal)} {a.Paciente?.Nome,-25} {a.Paciente.DataDeNascimento:d}");
            }
            Console.WriteLine();
        }

        private class AC : Input<AgendamentoBuilder>
        {
            /* 
            * As mensagens a serem impressas na interação com o usuário.
            * Cada mensagem representa uma propriedade da classe Agendamento.
            */
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

            /* 
            * As mensagens a serem impressas na interação com o usuário.
            * Cada mensagem representa uma propriedade da classe Agendamento.
            */
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

        private class LA1 : Input<AgendamentoBuilder>
        {
            /* 
            * As mensagens a serem impressas na interação com o usuário.
            * Cada mensagem representa uma propriedade da classe Agendamento.
            */
            protected override string[] Messages
            {
                get => _messages;
            }

            protected override Action<AgendamentoBuilder, string>[] Actions
            {
                get => _actions;
            }


            private readonly string[] _messages =  {
                "Data Inicial:"
            };

            private readonly Action<AgendamentoBuilder, string>[] _actions =
                {

                // Set Data da Consulta
                (cB, line) => { cB.SetDataDaConsulta(line); }

            };
        }

        private class LA2 : Input<AgendamentoBuilder>
        {
            /* 
            * As mensagens a serem impressas na interação com o usuário.
            * Cada mensagem representa uma propriedade da classe Agendamento.
            */
            protected override string[] Messages
            {
                get => _messages;
            }

            protected override Action<AgendamentoBuilder, string>[] Actions
            {
                get => _actions;
            }


            private readonly string[] _messages =  {
                "Data Final:"
            };

            private readonly Action<AgendamentoBuilder, string>[] _actions =
                {

                // Set Data da Consulta
                (cB, line) => { cB.SetDataDaConsulta(line); }

            };
        }
    }
}
