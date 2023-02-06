using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio1.Migrations
{
    /// <inheritdoc />
    public partial class PersistirAgendamentoFuturo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos");

            migrationBuilder.AddColumn<int>(
                name: "AgendamentoFuturoId",
                table: "Pacientes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_AgendamentoFuturoId",
                table: "Pacientes",
                column: "AgendamentoFuturoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Agendamentos_AgendamentoFuturoId",
                table: "Pacientes",
                column: "AgendamentoFuturoId",
                principalTable: "Agendamentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Agendamentos_AgendamentoFuturoId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_AgendamentoFuturoId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "AgendamentoFuturoId",
                table: "Pacientes");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
