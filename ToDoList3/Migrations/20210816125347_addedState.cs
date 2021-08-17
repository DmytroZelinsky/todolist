using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList3.Migrations
{
    public partial class addedState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Importance",
                table: "ToDoLists",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "ToDoLists",
                newName: "Importance");
        }
    }
}
