using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnaylandi",
                table: "Sinif",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SinifOnayciAdi",
                table: "Sinif",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SinifOnayciId",
                table: "Sinif",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SinifOnayciUsername",
                table: "Sinif",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DersOnayciAdi",
                table: "Dersler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DersOnayciId",
                table: "Dersler",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DersOnayciUsername",
                table: "Dersler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnaylandi",
                table: "Dersler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BolumOnayciAdi",
                table: "Bolumler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "BolumOnayciId",
                table: "Bolumler",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "BolumOnayciUsername",
                table: "Bolumler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnaylandi",
                table: "Bolumler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnaylandi",
                table: "Sinif");

            migrationBuilder.DropColumn(
                name: "SinifOnayciAdi",
                table: "Sinif");

            migrationBuilder.DropColumn(
                name: "SinifOnayciId",
                table: "Sinif");

            migrationBuilder.DropColumn(
                name: "SinifOnayciUsername",
                table: "Sinif");

            migrationBuilder.DropColumn(
                name: "DersOnayciAdi",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "DersOnayciId",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "DersOnayciUsername",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "IsOnaylandi",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "BolumOnayciAdi",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "BolumOnayciId",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "BolumOnayciUsername",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "IsOnaylandi",
                table: "Bolumler");
        }
    }
}
