using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProCardsNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CardImageChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Sides_SideId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Sides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SideId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "SideId",
                table: "Images",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "BackImageId",
                table: "Cards",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FrontImageId",
                table: "Cards",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ImagesData",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesData", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ImagesData_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CardId",
                table: "Images",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BackImageId",
                table: "Cards",
                column: "BackImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_FrontImageId",
                table: "Cards",
                column: "FrontImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Images_BackImageId",
                table: "Cards",
                column: "BackImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Images_FrontImageId",
                table: "Cards",
                column: "FrontImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Images_BackImageId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Images_FrontImageId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "ImagesData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CardId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Cards_BackImageId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_FrontImageId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BackImageId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "FrontImageId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "SideId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Images",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                columns: new[] { "CardId", "SideId" });

            migrationBuilder.CreateTable(
                name: "Sides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SideName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sides", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_SideId",
                table: "Images",
                column: "SideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Sides_SideId",
                table: "Images",
                column: "SideId",
                principalTable: "Sides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
