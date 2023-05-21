using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class TestOgrenciler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersOgrenci_Ogrenci_DersOgrencileriId",
                table: "DersOgrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_AbpUsers_UserId",
                table: "Ogrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_OgrenciSinif_Ogrenci_SinifOgrencilerId",
                table: "OgrenciSinif");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ogrenci",
                table: "Ogrenci");

            migrationBuilder.RenameTable(
                name: "Ogrenci",
                newName: "Ogrenciler");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_UserId",
                table: "Ogrenciler",
                newName: "IX_Ogrenciler_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ogrenciler",
                table: "Ogrenciler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DersOgrenci_Ogrenciler_DersOgrencileriId",
                table: "DersOgrenci",
                column: "DersOgrencileriId",
                principalTable: "Ogrenciler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenciler_AbpUsers_UserId",
                table: "Ogrenciler",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OgrenciSinif_Ogrenciler_SinifOgrencilerId",
                table: "OgrenciSinif",
                column: "SinifOgrencilerId",
                principalTable: "Ogrenciler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersOgrenci_Ogrenciler_DersOgrencileriId",
                table: "DersOgrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenciler_AbpUsers_UserId",
                table: "Ogrenciler");

            migrationBuilder.DropForeignKey(
                name: "FK_OgrenciSinif_Ogrenciler_SinifOgrencilerId",
                table: "OgrenciSinif");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ogrenciler",
                table: "Ogrenciler");

            migrationBuilder.RenameTable(
                name: "Ogrenciler",
                newName: "Ogrenci");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenciler_UserId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ogrenci",
                table: "Ogrenci",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DersOgrenci_Ogrenci_DersOgrencileriId",
                table: "DersOgrenci",
                column: "DersOgrencileriId",
                principalTable: "Ogrenci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_AbpUsers_UserId",
                table: "Ogrenci",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OgrenciSinif_Ogrenci_SinifOgrencilerId",
                table: "OgrenciSinif",
                column: "SinifOgrencilerId",
                principalTable: "Ogrenci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
