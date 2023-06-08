using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class SoruCevaplarOgrenciId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OgrenciCevap",
                table: "Sorular");

            migrationBuilder.AddColumn<Guid>(
                name: "OgrenciId",
                table: "SoruCevaplar",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OgrenciId",
                table: "SoruCevaplar");

            migrationBuilder.AddColumn<Guid>(
                name: "OgrenciCevap",
                table: "Sorular",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
