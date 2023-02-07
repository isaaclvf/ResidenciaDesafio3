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
            PacienteBuilder p;
            AgendamentoBuilder a;
            switch(_ui.Menu()) 
            {
                case '0':
                    break;
                case '1':
                    p = _pv.CadastrarPaciente();
                    if(p is null) return;
                    try {
                        _pc.CadastrarPaciente(p);
                        _ui.Show(0, "Paciente Cadastrado com Sucesso!");
                    }catch (Paciente.InvalidPacienteException e)
                    {
                        _ui.Show(1, e.Message);
                    }
                    catch (Exception)
                    {
                        _ui.Show(2);
                    }
                    _ui.CadastroDePacientes();
                    break;
                case '2':
                    p = _pv.ExcluirPaciente();
                    if(p is null) return;
                    try {
                        _pc.ExcluirPaciente(p);
                        _ui.Show(0, "Paciente Excluído com Sucesso!");
                    }catch (Paciente.InvalidPacienteException e)
                    {
                        _ui.Show(1, e.Message);
                    }
                    catch (Exception)
                    {
                        _ui.Show(2);
                    }
                    _ui.CadastroDePacientes();
                    break;
                case '3':
                    _pv.ListarPacientesPorCpf(_pc.GetPacientes());
                    _ui.CadastroDePacientes();
                    break;
                case '4':
                    _pv.ListarPacientesPorNome(_pc.GetPacientes());
                    _ui.CadastroDePacientes();
                    break;
                 case '5':
                    a = _av.AgendarConsulta();
                    if(a is null) return;
                    try {
                        _ac.AgendarConsulta(a);
                        _ui.Show(0, "Consulta Agendada com Sucesso!");
                    }catch (Paciente.InvalidPacienteException e)
                    {
                        _ui.Show(1, e.Message);
                    }
                    catch (Exception)
                    {
                        _ui.Show(2);
                    }
                    _ui.Agenda();
                    break;
                case '6':
                    a = _av.CancelarConsulta();
                    if(a is null) return;
                    try {
                        _ac.CancelarConsulta(a);
                        _ui.Show(0, "Consulta Cancelada com Sucesso!");
                    }catch (Paciente.InvalidPacienteException e)
                    {
                        _ui.Show(1, e.Message);
                    }
                    catch (Exception)
                    {
                        _ui.Show(2);
                    }
                    _ui.Agenda();
                    break;
                case '7':
                    _av.ListarAgendaInteira(_ac.GetAgendamentos());
                    _ui.Agenda();
                    break;
                case '8':
                    var x = _av.ListarAgendaParcial();
                    _av.ListarPacialmente(_ac.GetAgendamentos(), x);
                    _ui.Agenda();
                    break;
            }
        }
    }
}