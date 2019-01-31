using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Core_Ajax.Migrations
{
    public partial class AddNewPropertyInOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Owners",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Owners");
        }
    }
}
