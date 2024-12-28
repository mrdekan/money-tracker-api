using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace money_tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetCurrency",
                table: "jars");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "currency_balances");

            migrationBuilder.AddColumn<int>(
                name: "TargetCurrencyId",
                table: "jars",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "currency_balances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CC = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_jars_TargetCurrencyId",
                table: "jars",
                column: "TargetCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_currency_balances_CurrencyId",
                table: "currency_balances",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_currency_balances_Currencies_CurrencyId",
                table: "currency_balances",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jars_Currencies_TargetCurrencyId",
                table: "jars",
                column: "TargetCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_currency_balances_Currencies_CurrencyId",
                table: "currency_balances");

            migrationBuilder.DropForeignKey(
                name: "FK_jars_Currencies_TargetCurrencyId",
                table: "jars");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_jars_TargetCurrencyId",
                table: "jars");

            migrationBuilder.DropIndex(
                name: "IX_currency_balances_CurrencyId",
                table: "currency_balances");

            migrationBuilder.DropColumn(
                name: "TargetCurrencyId",
                table: "jars");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "currency_balances");

            migrationBuilder.AddColumn<string>(
                name: "TargetCurrency",
                table: "jars",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "currency_balances",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }
    }
}
