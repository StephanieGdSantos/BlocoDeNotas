using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlocoDeNotas.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoDataNasNotas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataCriacao",
                table: "Nota",
                type: "text",
                nullable: false,
                defaultValue: ""); // Você pode definir um valor padrão, se necessário
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Nota");
        }
    }
}
