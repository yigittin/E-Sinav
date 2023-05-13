using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class DerslerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dersler_Bolumler_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DersOgrenci",
                columns: table => new
                {
                    DersOgrencileriId = table.Column<int>(type: "int", nullable: false),
                    DerslerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersOgrenci", x => new { x.DersOgrencileriId, x.DerslerId });
                    table.ForeignKey(
                        name: "FK_DersOgrenci_Dersler_DerslerId",
                        column: x => x.DerslerId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersOgrenci_Ogrenciler_DersOgrencileriId",
                        column: x => x.DersOgrencileriId,
                        principalTable: "Ogrenciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_BolumId",
                table: "Dersler",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_DersOgrenci_DerslerId",
                table: "DersOgrenci",
                column: "DerslerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DersOgrenci");

            migrationBuilder.DropTable(
                name: "Dersler");
        }
    }
}
