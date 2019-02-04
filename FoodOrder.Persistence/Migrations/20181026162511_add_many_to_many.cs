using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrder.Persistence.Migrations
{
    public partial class add_many_to_many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishItems_WeekDays_WeekDayId",
                table: "DishItems");

            migrationBuilder.DropIndex(
                name: "IX_DishItems_WeekDayId",
                table: "DishItems");

            migrationBuilder.DropColumn(
                name: "WeekDayId",
                table: "DishItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekDayId",
                table: "DishItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_WeekDayId",
                table: "DishItems",
                column: "WeekDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishItems_WeekDays_WeekDayId",
                table: "DishItems",
                column: "WeekDayId",
                principalTable: "WeekDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
