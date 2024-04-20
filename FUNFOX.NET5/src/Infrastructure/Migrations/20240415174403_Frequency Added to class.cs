using Microsoft.EntityFrameworkCore.Migrations;

namespace FUNFOX.NET5.Infrastructure.Migrations
{
    public partial class FrequencyAddedtoclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Classes");
        }
    }
}
