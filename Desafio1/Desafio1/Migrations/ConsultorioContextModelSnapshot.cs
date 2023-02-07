﻿// <auto-generated />
using System;
using Desafio1.Data.Persistent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Desafio1.Migrations
{
    [DbContext(typeof(ConsultorioContext))]
    partial class ConsultorioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Desafio1.Models.Agendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CpfDoPaciente")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataDaConsulta")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HorarioFinal")
                        .HasColumnType("integer");

                    b.Property<int>("HorarioInicial")
                        .HasColumnType("integer");

                    b.Property<int?>("PacienteId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("Desafio1.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AgendamentoFuturoId")
                        .HasColumnType("integer");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AgendamentoFuturoId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Desafio1.Models.Agendamento", b =>
                {
                    b.HasOne("Desafio1.Models.Paciente", "Paciente")
                        .WithMany("Agendamentos")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Desafio1.Models.Paciente", b =>
                {
                    b.HasOne("Desafio1.Models.Agendamento", "AgendamentoFuturo")
                        .WithMany()
                        .HasForeignKey("AgendamentoFuturoId");

                    b.Navigation("AgendamentoFuturo");
                });

            modelBuilder.Entity("Desafio1.Models.Paciente", b =>
                {
                    b.Navigation("Agendamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
