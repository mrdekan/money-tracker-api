using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace money_tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "jars",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Target",
                table: "jars",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TargetCurrency",
                table: "jars",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "jars");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "jars");

            migrationBuilder.DropColumn(
                name: "TargetCurrency",
                table: "jars");
        }
    }
}
