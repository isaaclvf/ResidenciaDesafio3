using Microsoft.EntityFrameworkCore;
using dotenv.net;
using System;

namespace Desafio1.Data.Entity
{

    public static class PostgresConsultorioContext
    {

        public static string Host, Database, Username, Port;
        private static string Password;

        static PostgresConsultorioContext() 
        {
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {"./.env", System.IO.Directory.GetParent(Environment.CurrentDirectory).FullName + "/.env"}));

            Host     = Environment.GetEnvironmentVariable("HOST");
            Database = Environment.GetEnvironmentVariable("DATABASE");
            Username = Environment.GetEnvironmentVariable("USERNAME");
            Password = Environment.GetEnvironmentVariable("PASSWORD");
            Port     = Environment.GetEnvironmentVariable("PORT");

        }

        public static string Connection() 
        {
            return $"Host={Host}:{Port};Database={Database};Username={Username};Password={Password}";
        }

        public static EntityContext Build() 
        {
            var contextOptions = new DbContextOptionsBuilder<EntityContext>()
            .UseNpgsql($"Host={Host}:{Port};Database={Database};Username={Username};Password={Password}")
            .Options;
            return new EntityContext(contextOptions);
        }
    }
}