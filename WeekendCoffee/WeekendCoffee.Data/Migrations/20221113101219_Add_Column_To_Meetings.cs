using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeekendCoffee.Data.Migrations
{
    public partial class Add_Column_To_Meetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Meetings");
        }
    }
}
