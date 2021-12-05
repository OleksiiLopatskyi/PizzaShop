using Microsoft.EntityFrameworkCore.Migrations;

namespace FillPizzaShop.Migrations
{
    public partial class DiscountSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "UserAccounts");
        }
    }
}
