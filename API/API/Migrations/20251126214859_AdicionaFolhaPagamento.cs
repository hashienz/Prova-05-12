using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaFolhaPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folhas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Mes = table.Column<int>(type: "INTEGER", nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    HorasTrabalhadas = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorHora = table.Column<double>(type: "REAL", nullable: false),
                    SalarioBruto = table.Column<double>(type: "REAL", nullable: false),
                    ImpostoRenda = table.Column<double>(type: "REAL", nullable: false),
                    Inss = table.Column<double>(type: "REAL", nullable: false),
                    SalarioLiquido = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folhas", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1,
                column: "CriadoEm",
                value: new DateTime(2025, 11, 26, 18, 48, 59, 704, DateTimeKind.Local).AddTicks(387));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2,
                column: "CriadoEm",
                value: new DateTime(2025, 11, 26, 18, 48, 59, 704, DateTimeKind.Local).AddTicks(388));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 3,
                column: "CriadoEm",
                value: new DateTime(2025, 11, 26, 18, 48, 59, 704, DateTimeKind.Local).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "TarefaId",
                keyValue: "2f1b7dc1-3b9a-4e1a-a389-7f5d2f1c8f3e",
                column: "CriadoEm",
                value: new DateTime(2025, 11, 29, 18, 48, 59, 704, DateTimeKind.Local).AddTicks(565));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "TarefaId",
                keyValue: "6a8b3e4d-5e4e-4f7e-bdc9-9181e456ad0e",
                column: "CriadoEm",
                value: new DateTime(2025, 12, 3, 18, 48, 59, 704, DateTimeKind.Local).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "TarefaId",
                keyValue: "e5d4a7b9-1f9e-4c4a-ae3b-5b7c1a9d2e3f",
                column: "CriadoEm",
                value: new DateTime(2025, 12, 10, 18, 48, 59, 704, DateTimeKind.Local).AddTicks(568));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Folhas");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1,
                column: "CriadoEm",
                value: new DateTime(2025, 11, 26, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(5948));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2,
                column: "CriadoEm",
                value: new DateTime(2025, 11, 26, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(5949));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 3,
                column: "CriadoEm",
                value: new DateTime(2025, 11, 26, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(5950));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "TarefaId",
                keyValue: "2f1b7dc1-3b9a-4e1a-a389-7f5d2f1c8f3e",
                column: "CriadoEm",
                value: new DateTime(2025, 11, 29, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(6132));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "TarefaId",
                keyValue: "6a8b3e4d-5e4e-4f7e-bdc9-9181e456ad0e",
                column: "CriadoEm",
                value: new DateTime(2025, 12, 3, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(6118));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "TarefaId",
                keyValue: "e5d4a7b9-1f9e-4c4a-ae3b-5b7c1a9d2e3f",
                column: "CriadoEm",
                value: new DateTime(2025, 12, 10, 18, 20, 19, 743, DateTimeKind.Local).AddTicks(6135));
        }
    }
}
