using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanalVaka.Migrations
{
    /// <inheritdoc />
    public partial class DersOgrenciList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DersId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_DersId",
                table: "AbpUsers",
                column: "DersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Dersler_DersId",
                table: "AbpUsers",
                column: "DersId",
                principalTable: "Dersler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Dersler_DersId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_DersId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DersId",
                table: "AbpUsers");
        }
    }
}
