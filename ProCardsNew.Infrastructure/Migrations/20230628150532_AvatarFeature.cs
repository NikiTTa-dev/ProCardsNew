using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProCardsNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AvatarFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarNumber",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarNumber",
                table: "Users");
        }
    }
}
