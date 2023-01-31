using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Desafio1.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pacientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    datadenascimento = table.Column<DateTime>(name: "data_de_nascimento", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pacientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agendamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cpfdopaciente = table.Column<string>(name: "cpf_do_paciente", type: "text", nullable: false),
                    datadaconsulta = table.Column<DateTime>(name: "data_da_consulta", type: "timestamp with time zone", nullable: false),
                    horarioinicial = table.Column<int>(name: "horario_inicial", type: "integer", nullable: false),
                    horariofinal = table.Column<int>(name: "horario_final", type: "integer", nullable: false),
                    pacienteid = table.Column<int>(name: "paciente_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agendamentos", x => x.id);
                    table.ForeignKey(
                        name: "fk_agendamentos_pacientes_paciente_id",
                        column: x => x.pacienteid,
                        principalTable: "pacientes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_agendamentos_paciente_id",
                table: "agendamentos",
                column: "paciente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendamentos");

            migrationBuilder.DropTable(
                name: "pacientes");
        }
    }
}
