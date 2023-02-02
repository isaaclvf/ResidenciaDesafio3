using System;
using System.Linq;
using Desafio1.Models;
using Desafio1.Controllers;

namespace Desafio1.Views
{
    public class PacienteView
    {
        private readonly CP cadastrarPaciente = new();
        private readonly EP excluirPaciente = new();

        private readonly PacienteController pc = new();
        public void CadastrarPaciente()
        {
            PacienteBuilder pb = new();
            try
            {
                cadastrarPaciente.Read(pb);
                pc.CadastrarPaciente(pb);
                Console.WriteLine("\nSUCESSO:\tPaciente Cadastrado com Sucesso!\n");
            }catch(Paciente.InvalidPacienteException e)
            {
                Console.WriteLine($"\nERRO:\t{e.Message}\n");
            }catch(Exception)
            {
                Console.WriteLine("\nFATAL:\tErro inesperado ocorreu\n");
            }
        }

        public void ExcluirPaciente()
        {
            PacienteBuilder pb = new();
            try
            {
                excluirPaciente.Read(pb);
                pc.ExcluirPaciente(pb);
                Console.WriteLine("\nSUCESSO:\tPaciente Excluído com Sucesso!\n");
            }
            catch (Paciente.InvalidPacienteException e)
            {
                Console.WriteLine($"\nERRO:\t{e.Message}\n");
            }
            catch (Exception)
            {
                Console.WriteLine("\nFATAL:\tErro inesperado ocorreu\n");
            }
        }

        public void ListarPacientesPorCpf()
        {
            var tmp = pc.GetPacientes().OrderBy(x => x.Cpf);
            ImprimirLista(tmp);
        }

        public void ListarPacientesPorNome()
        {
            var tmp = pc.GetPacientes().OrderBy(x => x.Nome);
            ImprimirLista(tmp);
        }

        private static void ImprimirLista(IOrderedEnumerable<Paciente> list)
        {
            var border = new string('-', 57);

            Console.WriteLine(border);
            Console.WriteLine($"{"CPF",-12} {"Nome",-25} {"Dt.Nasc.", 9} {"Idade", 8}");
            Console.WriteLine(border);

            var now = DateTime.Now;
            foreach (var p in list)
            {
                var idade = new DateTime(now.Subtract(p.DataDeNascimento).Ticks).Year - 1;
                Console.WriteLine($"{p.Cpf,-12} {p.Nome,-25} {p.DataDeNascimento,0:d} {idade,7}");
                var tmp = p.AgendamentoFuturo;
                if(tmp is not null)
                    Console.WriteLine($"{" ", -12} {$"Agendado para: {tmp.DataDaConsulta:d}\n{" ",-12} {tmp.HorarioInicial.String()} às {tmp.HorarioFinal.String()}", -25}");
            }
            Console.WriteLine();
        }


        private class CP : Input<PacienteBuilder>
        {

            protected override string[] Messages
            {
                get => _messages;
            }

            protected override Action<PacienteBuilder, string>[] Actions
            {
                get => _actions;
            }

            /* 
            * As mensagens a serem impressas na interação com o usuário.
            * Cada mensagem representa uma propriedade da classe Paciente.
            */
            protected string[] _messages =
                {
                "Nome:",
                "CPF:",
                "Data de Nascimento:"
            };

            protected Action<PacienteBuilder, string>[] _actions =
                {

                // Set Nome
                (cB, line) => { cB.SetNome(line); },

                // Set Cpf
                (cB, line) => { cB.SetCpf(line); },

                // Set Data de Nascimento
                (cB, line) => { cB.SetDataDeNascimento(line); },

            };
        }

        private class EP : Input<PacienteBuilder>
        {
            protected override string[] Messages
            {
                get => _messages;
            }

            protected override Action<PacienteBuilder, string>[] Actions
            {
                get => _actions;
            }

            /* 
            * As mensagens a serem impressas na interação com o usuário.
            * Cada mensagem representa uma propriedade da classe Paciente.
            */
            protected string[] _messages = { "CPF:" };

            protected Action<PacienteBuilder, string>[] _actions = {
                // Set CPF
                (cB, line) => { cB.SetCpf(line); }
            };
        }
    }
}
