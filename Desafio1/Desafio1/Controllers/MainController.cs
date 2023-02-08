using System;
using Desafio1.Models;
using Desafio1.Views;

namespace Desafio1.Controllers
{
    public class MainController
    {
        private readonly UserInterface _ui = new();

        PacienteController _pacienteController;

        AgendamentoController _agendamentoController;

        AgendamentoView _agendamentoView = new();

        PacienteView _pacienteView = new();

        public MainController(Consultorio consultorio)
        {
            _pacienteController = new(consultorio);
            _agendamentoController = new(consultorio);
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
                    HelperPaciente(_pacienteView.CadastrarPaciente, _pacienteController.CadastrarPaciente, "Paciente Cadastrado com Sucesso!");
                    t = _ui.CadastroDePacientes();
                    break;
                case '2':
                    HelperPaciente(_pacienteView.ExcluirPaciente, _pacienteController.ExcluirPaciente, "Paciente Excluído com Sucesso!");
                    t = _ui.CadastroDePacientes();
                    break;
                case '3':
                    _pacienteView.ListarPacientesPorCpf(_pacienteController.GetPacientes());
                    t = _ui.CadastroDePacientes();
                    break;
                case '4':
                    _pacienteView.ListarPacientesPorNome(_pacienteController.GetPacientes());
                    t = _ui.CadastroDePacientes();
                    break;
                 case '5':
                    HelperAgendamento(_agendamentoView.AgendarConsulta, _agendamentoController.AgendarConsulta, "Consulta Agendada com Sucesso!");
                    t = _ui.Agenda();
                    break;
                case '6':
                    HelperAgendamento(_agendamentoView.CancelarConsulta, _agendamentoController.CancelarConsulta, "Consulta Cancelada com Sucesso!");
                    t = _ui.Agenda();
                    break;
                case '7':
                    _agendamentoView.ListarAgendaInteira(_agendamentoController.GetAgendamentos());
                    t = _ui.Agenda();
                    break;
                case '8':
                    var x = _agendamentoView.ListarAgendaParcial();
                    _agendamentoView.ListarPacialmente(_agendamentoController.GetAgendamentos(), x);
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
            }catch (Agendamento.InvalidAgendamentoException  e)
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