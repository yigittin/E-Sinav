using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class TumYetkililer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dersler_DersYetkililer_DersYetkiliId",
                table: "Dersler");

            migrationBuilder.DropForeignKey(
                name: "FK_SinifYetkililer_Dersler_DersId",
                table: "SinifYetkililer");

            migrationBuilder.DropIndex(
                name: "IX_SinifYetkililer_DersId",
                table: "SinifYetkililer");

            migrationBuilder.DropIndex(
                name: "IX_Dersler_DersYetkiliId",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "DersId",
                table: "SinifYetkililer");

            migrationBuilder.DropColumn(
                name: "DersYetkiliId",
                table: "Dersler");

            migrationBuilder.AddColumn<int>(
                name: "DersYetkiliId",
                table: "DersYetkililer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DersDersYetkili",
                columns: table => new
                {
                    DersYetkililerId = table.Column<int>(type: "int", nullable: false),
                    DerslerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersDersYetkili", x => new { x.DersYetkililerId, x.DerslerId });
                    table.ForeignKey(
                        name: "FK_DersDersYetkili_DersYetkililer_DersYetkililerId",
                        column: x => x.DersYetkililerId,
                        principalTable: "DersYetkililer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersDersYetkili_Dersler_DerslerId",
                        column: x => x.DerslerId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DersYetkililer_DersYetkiliId",
                table: "DersYetkililer",
                column: "DersYetkiliId");

            migrationBuilder.CreateIndex(
                name: "IX_DersDersYetkili_DerslerId",
                table: "DersDersYetkili",
                column: "DerslerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DersYetkililer_DersYetkililer_DersYetkiliId",
                table: "DersYetkililer",
                column: "DersYetkiliId",
                principalTable: "DersYetkililer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersYetkililer_DersYetkililer_DersYetkiliId",
                table: "DersYetkililer");

            migrationBuilder.DropTable(
                name: "DersDersYetkili");

            migrationBuilder.DropIndex(
                name: "IX_DersYetkililer_DersYetkiliId",
                table: "DersYetkililer");

            migrationBuilder.DropColumn(
                name: "DersYetkiliId",
                table: "DersYetkililer");

            migrationBuilder.AddColumn<Guid>(
                name: "DersId",
                table: "SinifYetkililer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DersYetkiliId",
                table: "Dersler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinifYetkililer_DersId",
                table: "SinifYetkililer",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_DersYetkiliId",
                table: "Dersler",
                column: "DersYetkiliId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dersler_DersYetkililer_DersYetkiliId",
                table: "Dersler",
                column: "DersYetkiliId",
                principalTable: "DersYetkililer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SinifYetkililer_Dersler_DersId",
                table: "SinifYetkililer",
                column: "DersId",
                principalTable: "Dersler",
                principalColumn: "Id");
        }
    }
}
