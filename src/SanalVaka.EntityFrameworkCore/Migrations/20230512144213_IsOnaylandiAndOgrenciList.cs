using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class IsOnaylandiAndOgrenciList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnaylandi",
                table: "Siniflar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OnaylayanKullaniciId",
                table: "Siniflar",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsOnaylandi",
                table: "Bolumler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SinifId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siniflar_OnaylayanKullaniciId",
                table: "Siniflar",
                column: "OnaylayanKullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_SinifId",
                table: "AbpUsers",
                column: "SinifId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Siniflar_SinifId",
                table: "AbpUsers",
                column: "SinifId",
                principalTable: "Siniflar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Siniflar_AbpUsers_OnaylayanKullaniciId",
                table: "Siniflar",
                column: "OnaylayanKullaniciId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Siniflar_SinifId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Siniflar_AbpUsers_OnaylayanKullaniciId",
                table: "Siniflar");

            migrationBuilder.DropIndex(
                name: "IX_Siniflar_OnaylayanKullaniciId",
                table: "Siniflar");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_SinifId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "IsOnaylandi",
                table: "Siniflar");

            migrationBuilder.DropColumn(
                name: "OnaylayanKullaniciId",
                table: "Siniflar");

            migrationBuilder.DropColumn(
                name: "IsOnaylandi",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "SinifId",
                table: "AbpUsers");
        }
    }
}
