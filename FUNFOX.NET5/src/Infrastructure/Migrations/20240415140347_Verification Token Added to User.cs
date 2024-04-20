using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FUNFOX.NET5.Infrastructure.Migrations
{
    public partial class VerificationTokenAddedtoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VerificationToken",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationToken",
                schema: "Identity",
                table: "Users");
        }
    }
}
