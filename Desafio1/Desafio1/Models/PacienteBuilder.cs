using System;
using System.Globalization;

namespace Desafio1.Models
{
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
                throw new Paciente.InvalidPacienteException("Nome", nome, "Nome do Cilente deve ter ao menos 5 caracteres");
            }
            return this;
        }

        // Valida e preenche CPF do Paciente
        public PacienteBuilder SetCpf(string value)
        {
            try
            {
                if (value.IsValidCpf())
                    paciente.Cpf = ulong.Parse(value);
            }
            catch (Exception e)
            {
                throw new Paciente.InvalidPacienteException("Cpf", value, e.Message);
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
                throw new Paciente.InvalidPacienteException("Data de Nascimento", value, "Formato de data inválido");
            }
            catch (Exception)
            {
                throw new Paciente.InvalidPacienteException("Data de Nascimento", value, "Idade do cliente deve ser maior ou igual a 13");
            }
            return this;
        }

        public Paciente Build()
        {
            var tmp = this.paciente;
            // this.paciente = new();
            return tmp;
        }
    }
}