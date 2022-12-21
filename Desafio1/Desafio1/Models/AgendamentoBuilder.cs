using Desafio1.Data.NonPersistent;
using System;
using System.Globalization;
using System.Linq;

namespace Desafio1.Models
{
    public class AgendamentoBuilder
    {

        private Agendamento agendamento = new();

        public ulong Cpf { get => agendamento.CpfDoPaciente; }
        public DateTime DataDaConsulta { get => agendamento.DataDaConsulta; }
        public ushort HoraInicial { get => agendamento.HoraInicial; }

        // Valida e preenche CPF do Paciente
        public void SetCpf(string value)
        {
            try
            {
                if (value.IsValidCpf())
                    agendamento.CpfDoPaciente = ulong.Parse(value);
            }
            catch (Exception e)
            {
                throw new Agendamento.InvalidAgendamentoException(e.Message);
            }
        }

        // Valida e preenche Data da Consulta do Agendamento
        public void SetDataDaConsulta(string value)
        {
            try
            {
                var date = DateTime.ParseExact(value, "d", new CultureInfo("pt-BR"));
                if(Agendamento.IsDateFuture(date, agendamento.HoraInicial))
                    agendamento.DataDaConsulta = date;
                else
                    throw new Agendamento.InvalidAgendamentoException("Data da Consulta deve ser futura");
            }
            catch (FormatException)
            {
                throw new Agendamento.InvalidAgendamentoException("Formato de data informado é inválido");
            }
        }

        // Valida e preenche Hora Inicial do Agendamento
        public void SetHoraInicial(string val)
        {
            try
            {
                if (val.IsValidHora())
                {
                    var tmp = ushort.Parse(val);
                    if (Agendamento.IsDateFuture(agendamento.DataDaConsulta, tmp))
                        agendamento.HoraInicial = tmp;
                    else
                        throw new Agendamento.InvalidAgendamentoException("Data da Consulta deve ser futura");
                }
            } catch (Exception e) when (e is FormatException || e is OverflowException)
            {
                throw new Agendamento.InvalidAgendamentoException("Formato de Hora informado é inválido");
            }
        }

        // Valida e preenche Hora Final do Agendamento
        public void SetHoraFinal(string val)
        {
            try
            {
                if (val.IsValidHora())
                {
                    var tmp = ushort.Parse(val);
                    if (tmp > agendamento.HoraInicial)
                        agendamento.HoraFinal = tmp;
                    else
                        throw new Agendamento.InvalidAgendamentoException("Hora Final deve ser posterior à Hora Inicial");
                }
            }
            catch (Exception e) when (e is FormatException || e is OverflowException)
            {
                throw new Agendamento.InvalidAgendamentoException("Formato de Hora informado é inválido");
            }
        }

        // Cria um objeto Agendamento
        public Agendamento Build()
        {
            var tmp = this.agendamento;
            this.agendamento = new();
            return tmp;
        }
    }
}
