using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class OgrenciHata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolumler_Ogrenciler_OgrenciId",
                table: "Bolumler");

            migrationBuilder.DropIndex(
                name: "IX_Bolumler_OgrenciId",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "OgrenciId",
                table: "Bolumler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OgrenciId",
                table: "Bolumler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bolumler_OgrenciId",
                table: "Bolumler",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolumler_Ogrenciler_OgrenciId",
                table: "Bolumler",
                column: "OgrenciId",
                principalTable: "Ogrenciler",
                principalColumn: "Id");
        }
    }
}
