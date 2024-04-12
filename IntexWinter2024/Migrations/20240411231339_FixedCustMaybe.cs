using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntexWinter2024.Migrations
{
    /// <inheritdoc />
    public partial class FixedCustMaybe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempCustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempCustomerId",
                table: "Orders");
        }
    }
}
