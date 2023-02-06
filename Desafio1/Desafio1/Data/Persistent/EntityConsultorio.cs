using Desafio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Desafio1.Data.Persistent
{
    public class EntityConsultorio : IConsultorio
    {

        private ConsultorioContext _context;

        // Conjunto de Pacientes
        private DbSet<Paciente> Pacientes { get =>  _context.Pacientes;}
        // Conjunto de Agendamentos Ordenados
        private DbSet<Agendamento> Agendamentos { get =>  _context.Agendamentos;}

        public EntityConsultorio(ConsultorioContext context) 
        { 
            _context = context;
        }

        // Métodos Públicos para a manipulação dos Dados (Agendamentos e Pacientes)

        public bool AddPaciente(Paciente p)
        {
            if(CpfExists(p.Cpf))
                return false;

            Pacientes.Add(p);
            _context.SaveChanges();

            return true;
        }

        public bool DeletePaciente(Paciente p)
        {
            if(!CpfExists(p.Cpf))
                return false;

            Pacientes.Remove(p);
            _context.SaveChanges();
            
            return true;
        }

        public IEnumerable<Paciente> GetAllPacientes()
        {
            return Pacientes.Include("AgendamentoFuturo").ToList();
        }
        public bool AddAgendamento(Agendamento a)
        {
            Agendamentos.Add(a);
            _context.SaveChanges();
            
            return true;
        }

        public bool DeleteAgendamento(string cpf, DateTime date, ushort hour)
        {

            var tmp = Agendamentos
                .FirstOrDefault(x => x.CpfDoPaciente == cpf && x.DataDaConsulta == date && x.HorarioInicial == hour);
            if (tmp is not null) {
                var x = Agendamentos.Remove(tmp).IsKeySet;
                _context.SaveChanges();
                return x;
            }
            return false;
        }

        public IEnumerable<Agendamento> GetAllAgendamentos()
        {
            return Agendamentos.Include("Paciente").ToList().OrderBy(a => a, new Agendamento.AgendamentoComparer()).ToList();
        }

        public bool CpfExists(string cpf)
        {
            return Pacientes.Any(x => x.Cpf == cpf);
        }

        public Paciente GetPacienteByCpf(string cpf)
        {
            return Pacientes.FirstOrDefault(x => x.Cpf == cpf);
        }

        public bool DeleteAllAgendamentosFromPaciente(Paciente p)
        {
            return true;
            // DbContext já faz isso (OnDelete Cascade)
        }

        public bool IsAgendamentoCadastrado(Agendamento a)
        {
            return Agendamentos.ToList().Any(x => a.Equals(x));
        }

        public bool UpdatePaciente(string cpf, Agendamento a)
        {
            var p = this.GetPacienteByCpf(cpf);
            try
            {
                p.AgendamentoFuturo = a;
                a.Paciente = p;
                _context.SaveChanges();
                return true;
            }catch(Exception)
            {
                throw new Agendamento.InvalidAgendamentoException("Paciente Informado já possui agendamento futuro");
            }

        }
    }
}
