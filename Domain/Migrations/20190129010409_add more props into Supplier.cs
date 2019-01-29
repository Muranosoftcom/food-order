using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addmorepropsintoSupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvailableMoneyToOrder",
                table: "Suppliers",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "CanMultiSelect",
                table: "Suppliers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableMoneyToOrder",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CanMultiSelect",
                table: "Suppliers");
        }
    }
}
