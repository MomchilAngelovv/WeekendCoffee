using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeekendCoffee.Data.Migrations
{
    public partial class Update_Meetings_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Meetings",
                newName: "Label");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Label",
                table: "Meetings",
                newName: "Name");
        }
    }
}
