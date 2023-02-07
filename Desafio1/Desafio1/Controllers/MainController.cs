using System;
using Desafio1.Models;
using Desafio1.Views;

namespace Desafio1.Controllers
{
    public class MainController
    {
        private readonly UserInterface _ui = new();

        PacienteController _pc;

        AgendamentoController _ac;

        AgendamentoView _av = new();

        PacienteView _pv = new();

        public MainController(Consultorio consultorio)
        {
            _pc = new(consultorio);
            _ac = new(consultorio);
        }

        public void Start() 
        {
            Loop(_ui.Menu());   
        }

        private void Loop(char tmp) {
            char t = '0';
            switch(tmp) 
            {
                case '1':
                    HelperPaciente(_pv.CadastrarPaciente, _pc.CadastrarPaciente, "Paciente Cadastrado com Sucesso!");
                    t = _ui.CadastroDePacientes();
                    break;
                case '2':
                    HelperPaciente(_pv.ExcluirPaciente, _pc.ExcluirPaciente, "Paciente Excluído com Sucesso!");
                    t = _ui.CadastroDePacientes();
                    break;
                case '3':
                    _pv.ListarPacientesPorCpf(_pc.GetPacientes());
                    t = _ui.CadastroDePacientes();
                    break;
                case '4':
                    _pv.ListarPacientesPorNome(_pc.GetPacientes());
                    t = _ui.CadastroDePacientes();
                    break;
                 case '5':
                    HelperAgendamento(_av.AgendarConsulta, _ac.AgendarConsulta, "Consulta Agendada com Sucesso!");
                    t = _ui.Agenda();
                    break;
                case '6':
                    HelperAgendamento(_av.CancelarConsulta, _ac.CancelarConsulta, "Consulta Cancelada com Sucesso!");
                    t = _ui.Agenda();
                    break;
                case '7':
                    _av.ListarAgendaInteira(_ac.GetAgendamentos());
                    t = _ui.Agenda();
                    break;
                case '8':
                    var x = _av.ListarAgendaParcial();
                    _av.ListarPacialmente(_ac.GetAgendamentos(), x);
                    t = _ui.Agenda();
                    break;
                default:
                    return;
            }
            Loop(t);
        }

        private void HelperPaciente(Func<PacienteBuilder> func1, Action<PacienteBuilder> action2, string mensagem) 
        {
            var p = func1();
            if(p is null) return;
            try {
                action2(p);
                _ui.Show(0, mensagem);
            }catch (Paciente.InvalidPacienteException  e)
            {
                _ui.Show(1, e.Message);
            }
            catch (Exception)
            {
                _ui.Show(2);
            }
        }

        private void HelperAgendamento(Func<AgendamentoBuilder> func1, Action<AgendamentoBuilder> action2, string mensagem) 
        {
            var a = func1();
            if(a is null) return;
            try {
                action2(a);
                _ui.Show(0, mensagem);
            }catch (Paciente.InvalidPacienteException  e)
            {
                _ui.Show(1, e.Message);
            }
            catch (Exception)
            {
                _ui.Show(2);
            }
        }
    }
}