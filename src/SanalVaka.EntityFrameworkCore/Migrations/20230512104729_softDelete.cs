using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class softDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Siniflar",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Siniflar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Siniflar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Dersler",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Dersler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dersler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Bolumler",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Bolumler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bolumler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Siniflar");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Siniflar");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Siniflar");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dersler");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bolumler");
        }
    }
}
