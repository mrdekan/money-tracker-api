using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace money_tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FieldsUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_currency_balances_Currencies_CurrencyId",
                table: "currency_balances");

            migrationBuilder.DropForeignKey(
                name: "FK_currency_balances_users_UserId",
                table: "currency_balances");

            migrationBuilder.DropForeignKey(
                name: "FK_jars_Currencies_TargetCurrencyId",
                table: "jars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "jars");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "currencies");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "currency_balances",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_currencies",
                table: "currencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_currency_balances_currencies_CurrencyId",
                table: "currency_balances",
                column: "CurrencyId",
                principalTable: "currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_currency_balances_users_UserId",
                table: "currency_balances",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_jars_currencies_TargetCurrencyId",
                table: "jars",
                column: "TargetCurrencyId",
                principalTable: "currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_currency_balances_currencies_CurrencyId",
                table: "currency_balances");

            migrationBuilder.DropForeignKey(
                name: "FK_currency_balances_users_UserId",
                table: "currency_balances");

            migrationBuilder.DropForeignKey(
                name: "FK_jars_currencies_TargetCurrencyId",
                table: "jars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_currencies",
                table: "currencies");

            migrationBuilder.RenameTable(
                name: "currencies",
                newName: "Currencies");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "jars",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "currency_balances",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_currency_balances_Currencies_CurrencyId",
                table: "currency_balances",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_currency_balances_users_UserId",
                table: "currency_balances",
                column: "UserId",
                principalTable: "users",
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
    }
}
