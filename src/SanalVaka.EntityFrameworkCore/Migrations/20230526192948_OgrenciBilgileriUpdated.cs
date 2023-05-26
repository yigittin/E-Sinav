using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class OgrenciBilgileriUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersOgrenci_Dersler_DerslerId",
                table: "DersOgrenci");

            migrationBuilder.RenameColumn(
                name: "DerslerId",
                table: "DersOgrenci",
                newName: "OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_DersOgrenci_DerslerId",
                table: "DersOgrenci",
                newName: "IX_DersOgrenci_OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersOgrenci_Dersler_OgrenciId",
                table: "DersOgrenci",
                column: "OgrenciId",
                principalTable: "Dersler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersOgrenci_Dersler_OgrenciId",
                table: "DersOgrenci");

            migrationBuilder.RenameColumn(
                name: "OgrenciId",
                table: "DersOgrenci",
                newName: "DerslerId");

            migrationBuilder.RenameIndex(
                name: "IX_DersOgrenci_OgrenciId",
                table: "DersOgrenci",
                newName: "IX_DersOgrenci_DerslerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersOgrenci_Dersler_DerslerId",
                table: "DersOgrenci",
                column: "DerslerId",
                principalTable: "Dersler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
