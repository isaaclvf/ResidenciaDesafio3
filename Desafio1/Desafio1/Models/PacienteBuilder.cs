using System;
using System.Globalization;

namespace Desafio1.Models
{
    // Valida as propriedades da classe Paciente
    public class PacienteBuilder
    {
        private Paciente paciente = new();

        public ulong Cpf { get => paciente.Cpf; }

        // Valida e preenche Nome do Paciente
        public PacienteBuilder SetNome(string nome)
        {
            if (nome.Length >= 5)
            {
                paciente.Nome = nome;
            }
            else
            {
                throw new Paciente.InvalidPacienteException("Nome do Paciente deve ter ao menos 5 caracteres");
            }
            return this;
        }

        // Valida e preenche CPF do Paciente
        public PacienteBuilder SetCpf(string value)
        {
            try
            {
                // Usa método de extensão em CpfExtension
                if (value.IsValidCpf())
                    paciente.Cpf = ulong.Parse(value);
            }
            catch (Exception e)
            {
                throw new Paciente.InvalidPacienteException(e.Message);
            }
            return this;
        }

        // Valida e preenche Data de nascimento do Paciente
        public PacienteBuilder SetDataDeNascimento(string value)
        {
            try
            {
                var date = DateTime.ParseExact(value, "d", new CultureInfo("pt-BR"));
                var today = DateTime.Today;
                var age = today.Year - date.Year;
                if (date > today.AddYears(-age)) age--;

                if (age < 13)
                    throw new Exception();

                paciente.DataDeNascimento = date;
            }
            catch (FormatException)
            {
                throw new Paciente.InvalidPacienteException("Formato de data informado é inválido");
            }
            catch (Exception)
            {
                throw new Paciente.InvalidPacienteException("Idade do cliente deve ser maior ou igual a 13");
            }
            return this;
        }

        // Cria um objeto Paciente
        public Paciente Build()
        {
            var tmp = this.paciente;
            this.paciente = new();
            return tmp;
        }
    }
}