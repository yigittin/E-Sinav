using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class SiniflarVeYetkililer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DersYetkiliId",
                table: "Dersler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BolumYetkililer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretmenNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BolumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BolumYetkiliId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_BolumYetkililer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BolumYetkililer_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BolumYetkililer_BolumYetkililer_BolumYetkiliId",
                        column: x => x.BolumYetkiliId,
                        principalTable: "BolumYetkililer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BolumYetkililer_Bolumler_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolumler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DersYetkililer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretmenNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_DersYetkililer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DersYetkililer_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sinif",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SinifLimit = table.Column<int>(type: "int", nullable: false),
                    SinifName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Sinif", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SinifYetkililer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretmenNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DersId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_SinifYetkililer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinifYetkililer_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinifYetkililer_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OgrenciSinif",
                columns: table => new
                {
                    SinifOgrencilerId = table.Column<int>(type: "int", nullable: false),
                    SiniflarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciSinif", x => new { x.SinifOgrencilerId, x.SiniflarId });
                    table.ForeignKey(
                        name: "FK_OgrenciSinif_Ogrenciler_SinifOgrencilerId",
                        column: x => x.SinifOgrencilerId,
                        principalTable: "Ogrenciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrenciSinif_Sinif_SiniflarId",
                        column: x => x.SiniflarId,
                        principalTable: "Sinif",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinifSinifYetkili",
                columns: table => new
                {
                    SinifYetkililerId = table.Column<int>(type: "int", nullable: false),
                    SiniflarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinifSinifYetkili", x => new { x.SinifYetkililerId, x.SiniflarId });
                    table.ForeignKey(
                        name: "FK_SinifSinifYetkili_SinifYetkililer_SinifYetkililerId",
                        column: x => x.SinifYetkililerId,
                        principalTable: "SinifYetkililer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinifSinifYetkili_Sinif_SiniflarId",
                        column: x => x.SiniflarId,
                        principalTable: "Sinif",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_DersYetkiliId",
                table: "Dersler",
                column: "DersYetkiliId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumYetkililer_BolumId",
                table: "BolumYetkililer",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumYetkililer_BolumYetkiliId",
                table: "BolumYetkililer",
                column: "BolumYetkiliId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumYetkililer_UserId",
                table: "BolumYetkililer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DersYetkililer_UserId",
                table: "DersYetkililer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciSinif_SiniflarId",
                table: "OgrenciSinif",
                column: "SiniflarId");

            migrationBuilder.CreateIndex(
                name: "IX_SinifSinifYetkili_SiniflarId",
                table: "SinifSinifYetkili",
                column: "SiniflarId");

            migrationBuilder.CreateIndex(
                name: "IX_SinifYetkililer_DersId",
                table: "SinifYetkililer",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_SinifYetkililer_UserId",
                table: "SinifYetkililer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dersler_DersYetkililer_DersYetkiliId",
                table: "Dersler",
                column: "DersYetkiliId",
                principalTable: "DersYetkililer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dersler_DersYetkililer_DersYetkiliId",
                table: "Dersler");

            migrationBuilder.DropTable(
                name: "BolumYetkililer");

            migrationBuilder.DropTable(
                name: "DersYetkililer");

            migrationBuilder.DropTable(
                name: "OgrenciSinif");

            migrationBuilder.DropTable(
                name: "SinifSinifYetkili");

            migrationBuilder.DropTable(
                name: "SinifYetkililer");

            migrationBuilder.DropTable(
                name: "Sinif");

            migrationBuilder.DropIndex(
                name: "IX_Dersler_DersYetkiliId",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "DersYetkiliId",
                table: "Dersler");
        }
    }
}
