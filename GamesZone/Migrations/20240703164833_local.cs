using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesZone.Migrations
{
    /// <inheritdoc />
    public partial class local : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "UserId",
               table: "Game",
               type: "nvarchar(max)",
               nullable: false,
               defaultValue: "ad969a7e-9c6c-488a-8d9c-4eb0f92170ed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "UserId",
            table: "Game");
        }
    }
}
