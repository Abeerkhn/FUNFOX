using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FUNFOX.NET5.Infrastructure.Migrations
{
    public partial class ChangedTimingfromTimeSpanToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timing_StartTime",
                table: "Classes",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Timing_EndTime",
                table: "Classes",
                newName: "EndTime");

            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Classes",
                newName: "Timing_StartTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Classes",
                newName: "Timing_EndTime");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Timing_StartTime",
                table: "Classes",
                type: "time",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Timing_EndTime",
                table: "Classes",
                type: "time",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
