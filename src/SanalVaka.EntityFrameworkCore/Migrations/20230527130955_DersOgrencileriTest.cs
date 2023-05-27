using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class DersOgrencileriTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DersOgrenci",
                table: "DersOgrenci");

            migrationBuilder.RenameTable(
                name: "DersOgrenci",
                newName: "DersOgrenciler");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "DersOgrenciler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "DersOgrenciler",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "DersOgrenciler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "DersOgrenciler",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersOgrenciler",
                table: "DersOgrenciler",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DersOgrenciler",
                table: "DersOgrenciler");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "DersOgrenciler");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "DersOgrenciler");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "DersOgrenciler");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "DersOgrenciler");

            migrationBuilder.RenameTable(
                name: "DersOgrenciler",
                newName: "DersOgrenci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersOgrenci",
                table: "DersOgrenci",
                column: "Id");
        }
    }
}
