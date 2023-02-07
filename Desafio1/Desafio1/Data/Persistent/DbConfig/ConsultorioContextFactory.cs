using Microsoft.EntityFrameworkCore;
using dotenv.net;
using System;
using Microsoft.EntityFrameworkCore.Design;

namespace Desafio1.Data.Persistent.DbConfig
{

    public class ConsultorioContextFactory : IDesignTimeDbContextFactory<ConsultorioContext>
    {

        public static string Host, Database, Username, Port;
        private static string Password;

        static ConsultorioContextFactory() 
        {
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {".env", System.IO.Directory.GetParent(Environment.CurrentDirectory).FullName + "/.env"}));

            Host     = Environment.GetEnvironmentVariable("HOST");
            Database = Environment.GetEnvironmentVariable("DATABASE");
            Username = Environment.GetEnvironmentVariable("USERNAME");
            Password = Environment.GetEnvironmentVariable("PASSWORD");
            Port     = Environment.GetEnvironmentVariable("PORT");

        }

        public ConsultorioContext CreateDbContext(string[] args=null)
        {
            var contextOptions = new DbContextOptionsBuilder<ConsultorioContext>()
            .UseNpgsql($"Host={Host}:{Port};Database={Database};Username={Username};Password={Password}")
            // .UseSnakeCaseNamingConvention()
            .Options;
            return new ConsultorioContext(contextOptions);
        }
    }
}