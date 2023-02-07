using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio1.Migrations
{
    /// <inheritdoc />
    public partial class DeleçãoemCascata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id");
        }
    }
}
