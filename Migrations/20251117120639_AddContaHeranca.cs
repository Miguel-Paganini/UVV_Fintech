using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UVV_fintech.Migrations
{
    /// <inheritdoc />
    public partial class AddContaHeranca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TaxaManutencao",
                table: "Contas",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaRendimento",
                table: "Contas",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoConta",
                table: "Contas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxaManutencao",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "TaxaRendimento",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "TipoConta",
                table: "Contas");
        }
    }
}
