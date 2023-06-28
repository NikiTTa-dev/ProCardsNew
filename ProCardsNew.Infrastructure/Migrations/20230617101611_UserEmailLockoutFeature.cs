using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProCardsNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserEmailLockoutFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordRecoveryLastEmailSentDateTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordRecoveryLastEmailSentDateTime",
                table: "Users");
        }
    }
}
