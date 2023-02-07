using Desafio1.Controllers;
using Desafio1.Data.NonPersistent;
using Desafio1.Models;
using Desafio1.Data.Persistent;
using Desafio1.Data.Persistent.DbConfig;

namespace Desafio1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new MainController(
                new Consultorio(
                    new EntityConsultorio(
                        new ConsultorioContextFactory().CreateDbContext()
                    )
                )
            ).Start();
        }
    }
}
