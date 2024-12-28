using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace money_tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedStoreType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "stores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "stores",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
