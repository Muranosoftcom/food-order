using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrder.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    AvailableUntil = table.Column<DateTime>(nullable: false),
                    PositiveReviews = table.Column<int>(nullable: false),
                    NegativeReviews = table.Column<int>(nullable: false),
                    WeekDayId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishItems_WeekDays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DishItemsToWeekDays",
                columns: table => new
                {
                    DishItemId = table.Column<int>(nullable: false),
                    WeekDayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishItemsToWeekDays", x => new { x.DishItemId, x.WeekDayId });
                    table.ForeignKey(
                        name: "FK_DishItemsToWeekDays_DishItems_DishItemId",
                        column: x => x.DishItemId,
                        principalTable: "DishItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishItemsToWeekDays_WeekDays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_WeekDayId",
                table: "DishItems",
                column: "WeekDayId");

            migrationBuilder.CreateIndex(
                name: "IX_DishItemsToWeekDays_WeekDayId",
                table: "DishItemsToWeekDays",
                column: "WeekDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishItemsToWeekDays");

            migrationBuilder.DropTable(
                name: "DishItems");

            migrationBuilder.DropTable(
                name: "WeekDays");
        }
    }
}
