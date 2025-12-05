using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    TarefaId = table.Column<string>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.TarefaId);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CriadoEm", "Nome" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 26, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(5948), "Trabalho" },
                    { 2, new DateTime(2025, 11, 26, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(5949), "Estudos" },
                    { 3, new DateTime(2025, 11, 26, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(5950), "Lazer" }
                });

            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "TarefaId", "CategoriaId", "CriadoEm", "Descricao", "Status", "Titulo" },
                values: new object[,]
                {
                    { "2f1b7dc1-3b9a-4e1a-a389-7f5d2f1c8f3e", 2, new DateTime(2025, 11, 29, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(6132), null, "Não iniciada", "Estudar Angular" },
                    { "6a8b3e4d-5e4e-4f7e-bdc9-9181e456ad0e", 1, new DateTime(2025, 12, 3, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(6118), null, "Não iniciada", "Concluir relatório" },
                    { "e5d4a7b9-1f9e-4c4a-ae3b-5b7c1a9d2e3f", 3, new DateTime(2025, 12, 10, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(6135), null, "Não iniciada", "Passeio no parque" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Tarefas");
        }
    }
}
