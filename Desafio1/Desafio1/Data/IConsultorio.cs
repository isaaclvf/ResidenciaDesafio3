using System.Collections.Generic;
using Desafio1.Models;

namespace Desafio1.Data
{
    public interface IConsultorio : IPacienteDao, IAgendamentoDao{}
}