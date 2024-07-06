using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesZone.Migrations
{
    /// <inheritdoc />
    public partial class server : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "0f6be9f7-82c1-4add-8c15-c749a72c9206",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "ad969a7e-9c6c-488a-8d9c-4eb0f92170ed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "ad969a7e-9c6c-488a-8d9c-4eb0f92170ed",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "0f6be9f7-82c1-4add-8c15-c749a72c9206");
        }
    }
}
