using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class SinifDersAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "SinifOgrenciler",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "SinifOgrenciler",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "SinifOgrenciler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "SinifOgrenciler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SinifOgrenciler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "DersOgrenciler",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "DersOgrenciler",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "SinifOgrenciler");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "SinifOgrenciler");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "SinifOgrenciler");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "SinifOgrenciler");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SinifOgrenciler");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "DersOgrenciler");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "DersOgrenciler");
        }
    }
}
