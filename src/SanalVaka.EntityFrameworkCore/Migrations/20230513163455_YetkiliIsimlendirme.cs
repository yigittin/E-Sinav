using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class YetkiliIsimlendirme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BolumYetkililer_BolumYetkililer_BolumYetkiliId",
                table: "BolumYetkililer");

            migrationBuilder.DropForeignKey(
                name: "FK_BolumYetkililer_Bolumler_BolumId",
                table: "BolumYetkililer");

            migrationBuilder.DropForeignKey(
                name: "FK_DersDersYetkili_DersYetkililer_DersYetkililerId",
                table: "DersDersYetkili");

            migrationBuilder.DropForeignKey(
                name: "FK_SinifSinifYetkili_SinifYetkililer_SinifYetkililerId",
                table: "SinifSinifYetkili");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SinifSinifYetkili",
                table: "SinifSinifYetkili");

            migrationBuilder.DropIndex(
                name: "IX_SinifSinifYetkili_SiniflarId",
                table: "SinifSinifYetkili");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersDersYetkili",
                table: "DersDersYetkili");

            migrationBuilder.DropIndex(
                name: "IX_DersDersYetkili_DerslerId",
                table: "DersDersYetkili");

            migrationBuilder.DropIndex(
                name: "IX_BolumYetkililer_BolumId",
                table: "BolumYetkililer");

            migrationBuilder.DropIndex(
                name: "IX_BolumYetkililer_BolumYetkiliId",
                table: "BolumYetkililer");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "BolumYetkililer");

            migrationBuilder.DropColumn(
                name: "BolumYetkiliId",
                table: "BolumYetkililer");

            migrationBuilder.RenameColumn(
                name: "SinifYetkililerId",
                table: "SinifSinifYetkili",
                newName: "YetkililerId");

            migrationBuilder.RenameColumn(
                name: "DersYetkililerId",
                table: "DersDersYetkili",
                newName: "YetkililerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SinifSinifYetkili",
                table: "SinifSinifYetkili",
                columns: new[] { "SiniflarId", "YetkililerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersDersYetkili",
                table: "DersDersYetkili",
                columns: new[] { "DerslerId", "YetkililerId" });

            migrationBuilder.CreateTable(
                name: "BolumBolumYetkili",
                columns: table => new
                {
                    BolumlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YetkililerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolumBolumYetkili", x => new { x.BolumlerId, x.YetkililerId });
                    table.ForeignKey(
                        name: "FK_BolumBolumYetkili_BolumYetkililer_YetkililerId",
                        column: x => x.YetkililerId,
                        principalTable: "BolumYetkililer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BolumBolumYetkili_Bolumler_BolumlerId",
                        column: x => x.BolumlerId,
                        principalTable: "Bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinifSinifYetkili_YetkililerId",
                table: "SinifSinifYetkili",
                column: "YetkililerId");

            migrationBuilder.CreateIndex(
                name: "IX_DersDersYetkili_YetkililerId",
                table: "DersDersYetkili",
                column: "YetkililerId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumBolumYetkili_YetkililerId",
                table: "BolumBolumYetkili",
                column: "YetkililerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersDersYetkili_DersYetkililer_YetkililerId",
                table: "DersDersYetkili",
                column: "YetkililerId",
                principalTable: "DersYetkililer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinifSinifYetkili_SinifYetkililer_YetkililerId",
                table: "SinifSinifYetkili",
                column: "YetkililerId",
                principalTable: "SinifYetkililer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersDersYetkili_DersYetkililer_YetkililerId",
                table: "DersDersYetkili");

            migrationBuilder.DropForeignKey(
                name: "FK_SinifSinifYetkili_SinifYetkililer_YetkililerId",
                table: "SinifSinifYetkili");

            migrationBuilder.DropTable(
                name: "BolumBolumYetkili");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SinifSinifYetkili",
                table: "SinifSinifYetkili");

            migrationBuilder.DropIndex(
                name: "IX_SinifSinifYetkili_YetkililerId",
                table: "SinifSinifYetkili");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersDersYetkili",
                table: "DersDersYetkili");

            migrationBuilder.DropIndex(
                name: "IX_DersDersYetkili_YetkililerId",
                table: "DersDersYetkili");

            migrationBuilder.RenameColumn(
                name: "YetkililerId",
                table: "SinifSinifYetkili",
                newName: "SinifYetkililerId");

            migrationBuilder.RenameColumn(
                name: "YetkililerId",
                table: "DersDersYetkili",
                newName: "DersYetkililerId");

            migrationBuilder.AddColumn<Guid>(
                name: "BolumId",
                table: "BolumYetkililer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BolumYetkiliId",
                table: "BolumYetkililer",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SinifSinifYetkili",
                table: "SinifSinifYetkili",
                columns: new[] { "SinifYetkililerId", "SiniflarId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersDersYetkili",
                table: "DersDersYetkili",
                columns: new[] { "DersYetkililerId", "DerslerId" });

            migrationBuilder.CreateIndex(
                name: "IX_SinifSinifYetkili_SiniflarId",
                table: "SinifSinifYetkili",
                column: "SiniflarId");

            migrationBuilder.CreateIndex(
                name: "IX_DersDersYetkili_DerslerId",
                table: "DersDersYetkili",
                column: "DerslerId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumYetkililer_BolumId",
                table: "BolumYetkililer",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumYetkililer_BolumYetkiliId",
                table: "BolumYetkililer",
                column: "BolumYetkiliId");

            migrationBuilder.AddForeignKey(
                name: "FK_BolumYetkililer_BolumYetkililer_BolumYetkiliId",
                table: "BolumYetkililer",
                column: "BolumYetkiliId",
                principalTable: "BolumYetkililer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BolumYetkililer_Bolumler_BolumId",
                table: "BolumYetkililer",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DersDersYetkili_DersYetkililer_DersYetkililerId",
                table: "DersDersYetkili",
                column: "DersYetkililerId",
                principalTable: "DersYetkililer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinifSinifYetkili_SinifYetkililer_SinifYetkililerId",
                table: "SinifSinifYetkili",
                column: "SinifYetkililerId",
                principalTable: "SinifYetkililer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
