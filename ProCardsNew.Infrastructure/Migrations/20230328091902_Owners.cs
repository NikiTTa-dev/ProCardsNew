using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProCardsNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Owners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Decks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Cards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Decks_OwnerId",
                table: "Decks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_OwnerId",
                table: "Cards",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_OwnerId",
                table: "Cards",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Users_OwnerId",
                table: "Decks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_OwnerId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Users_OwnerId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Decks_OwnerId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Cards_OwnerId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cards");
        }
    }
}
