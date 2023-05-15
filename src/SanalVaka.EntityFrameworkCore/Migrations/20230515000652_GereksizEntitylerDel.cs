using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class GereksizEntitylerDel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_OgrenciSinif_Sinif_SiniflarId",
                table: "OgrenciSinif");

            migrationBuilder.DropTable(
                name: "BolumBolumYetkili");

            migrationBuilder.DropTable(
                name: "DersDersYetkili");

            migrationBuilder.DropTable(
                name: "SinifSinifYetkili");

            migrationBuilder.DropTable(
                name: "BolumYetkililer");

            migrationBuilder.DropTable(
                name: "DersYetkililer");

            migrationBuilder.DropTable(
                name: "SinifYetkililer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sinif",
                table: "Sinif");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ogrenciler",
                table: "Ogrenciler");

            migrationBuilder.RenameTable(
                name: "Sinif",
                newName: "Siniflar");

            migrationBuilder.RenameTable(
                name: "Ogrenciler",
                newName: "Ogrenci");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenciler_UserId",
                table: "Ogrenci",
                newName: "IX_Ogrenci_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "BolumId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DersId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SinifId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DersId",
                table: "Siniflar",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Siniflar",
                table: "Siniflar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ogrenci",
                table: "Ogrenci",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_BolumId",
                table: "AbpUsers",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_DersId",
                table: "AbpUsers",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_SinifId",
                table: "AbpUsers",
                column: "SinifId");

            migrationBuilder.CreateIndex(
                name: "IX_Siniflar_DersId",
                table: "Siniflar",
                column: "DersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Bolumler_BolumId",
                table: "AbpUsers",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Dersler_DersId",
                table: "AbpUsers",
                column: "DersId",
                principalTable: "Dersler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Siniflar_SinifId",
                table: "AbpUsers",
                column: "SinifId",
                principalTable: "Siniflar",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OgrenciSinif_Siniflar_SiniflarId",
                table: "OgrenciSinif",
                column: "SiniflarId",
                principalTable: "Siniflar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Siniflar_Dersler_DersId",
                table: "Siniflar",
                column: "DersId",
                principalTable: "Dersler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Bolumler_BolumId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Dersler_DersId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Siniflar_SinifId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DersOgrenci_Ogrenci_DersOgrencileriId",
                table: "DersOgrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_AbpUsers_UserId",
                table: "Ogrenci");

            migrationBuilder.DropForeignKey(
                name: "FK_OgrenciSinif_Ogrenci_SinifOgrencilerId",
                table: "OgrenciSinif");

            migrationBuilder.DropForeignKey(
                name: "FK_OgrenciSinif_Siniflar_SiniflarId",
                table: "OgrenciSinif");

            migrationBuilder.DropForeignKey(
                name: "FK_Siniflar_Dersler_DersId",
                table: "Siniflar");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_BolumId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_DersId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_SinifId",
                table: "AbpUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Siniflar",
                table: "Siniflar");

            migrationBuilder.DropIndex(
                name: "IX_Siniflar_DersId",
                table: "Siniflar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ogrenci",
                table: "Ogrenci");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DersId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "SinifId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DersId",
                table: "Siniflar");

            migrationBuilder.RenameTable(
                name: "Siniflar",
                newName: "Sinif");

            migrationBuilder.RenameTable(
                name: "Ogrenci",
                newName: "Ogrenciler");

            migrationBuilder.RenameIndex(
                name: "IX_Ogrenci_UserId",
                table: "Ogrenciler",
                newName: "IX_Ogrenciler_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sinif",
                table: "Sinif",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ogrenciler",
                table: "Ogrenciler",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BolumYetkililer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OgretmenNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "DersYetkililer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DersYetkiliId = table.Column<int>(type: "int", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OgretmenNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                    table.ForeignKey(
                        name: "FK_DersYetkililer_DersYetkililer_DersYetkiliId",
                        column: x => x.DersYetkiliId,
                        principalTable: "DersYetkililer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SinifYetkililer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OgretmenNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                });

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

            migrationBuilder.CreateTable(
                name: "DersDersYetkili",
                columns: table => new
                {
                    DerslerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YetkililerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersDersYetkili", x => new { x.DerslerId, x.YetkililerId });
                    table.ForeignKey(
                        name: "FK_DersDersYetkili_DersYetkililer_YetkililerId",
                        column: x => x.YetkililerId,
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

            migrationBuilder.CreateTable(
                name: "SinifSinifYetkili",
                columns: table => new
                {
                    SiniflarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YetkililerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinifSinifYetkili", x => new { x.SiniflarId, x.YetkililerId });
                    table.ForeignKey(
                        name: "FK_SinifSinifYetkili_SinifYetkililer_YetkililerId",
                        column: x => x.YetkililerId,
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
                name: "IX_BolumBolumYetkili_YetkililerId",
                table: "BolumBolumYetkili",
                column: "YetkililerId");

            migrationBuilder.CreateIndex(
                name: "IX_BolumYetkililer_UserId",
                table: "BolumYetkililer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DersDersYetkili_YetkililerId",
                table: "DersDersYetkili",
                column: "YetkililerId");

            migrationBuilder.CreateIndex(
                name: "IX_DersYetkililer_DersYetkiliId",
                table: "DersYetkililer",
                column: "DersYetkiliId");

            migrationBuilder.CreateIndex(
                name: "IX_DersYetkililer_UserId",
                table: "DersYetkililer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SinifSinifYetkili_YetkililerId",
                table: "SinifSinifYetkili",
                column: "YetkililerId");

            migrationBuilder.CreateIndex(
                name: "IX_SinifYetkililer_UserId",
                table: "SinifYetkililer",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OgrenciSinif_Sinif_SiniflarId",
                table: "OgrenciSinif",
                column: "SiniflarId",
                principalTable: "Sinif",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
