using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UVV_fintech.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Contas");

            migrationBuilder.AddColumn<string>(
                name: "TipoTransacao",
                table: "Transacoes",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTransacao",
                table: "Transacoes");

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "Contas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
