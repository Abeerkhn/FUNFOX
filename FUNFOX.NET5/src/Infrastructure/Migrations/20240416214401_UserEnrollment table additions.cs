using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FUNFOX.NET5.Infrastructure.Migrations
{
    public partial class UserEnrollmenttableadditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserClassEnrollments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserClassEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserClassEnrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "UserClassEnrollments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "UserClassEnrollments",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserClassEnrollments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserClassEnrollments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserClassEnrollments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "UserClassEnrollments");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "UserClassEnrollments");
        }
    }
}
