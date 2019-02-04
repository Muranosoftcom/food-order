using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrder.Persistence.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Supplier",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Столовая №1" },
                    { 2, "ГлаголЪ" }
                });

            migrationBuilder.InsertData(
                table: "WeekDays",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mon" },
                    { 2, "Tue" },
                    { 3, "Wed" },
                    { 4, "Thu" },
                    { 5, "Fri" },
                    { 6, "Sat" },
                    { 7, "Sun" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WeekDays",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
