using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "Pacientes",
                columns: table => new
                {
                    Cpf = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    DataDeNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    CpfDoPaciente = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    DataDaConsulta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HorarioInicial = table.Column<int>(type: "integer", nullable: false),
                    HorarioFinal = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => new { x.CpfDoPaciente, x.DataDaConsulta, x.HorarioInicial });
                    table.ForeignKey(
                        name: "FK_Agendamentos_Pacientes_CpfDoPaciente",
                        column: x => x.CpfDoPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_CpfDoPaciente",
                table: "Agendamentos",
                column: "CpfDoPaciente",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamentos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
