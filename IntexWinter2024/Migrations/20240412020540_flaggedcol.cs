using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntexWinter2024.Migrations
{
    /// <inheritdoc />
    public partial class flaggedcol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Flagged",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flagged",
                table: "Orders");
        }
    }
}
