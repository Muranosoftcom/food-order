using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrder.Persistence.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    CanMultiSelect = table.Column<bool>(nullable: false),
                    AvailableMoneyToOrder = table.Column<decimal>(type: "Money", nullable: false),
                    Position = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    SupplierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishCategories_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DishItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    AvailableUntil = table.Column<DateTime>(nullable: false),
                    PositiveReviews = table.Column<int>(nullable: false),
                    NegativeReviews = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishItems_DishCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DishCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DishItems_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    DishItemId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_DishItems_DishItemId",
                        column: x => x.DishItemId,
                        principalTable: "DishItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "AvailableMoneyToOrder", "CanMultiSelect", "Name" },
                values: new object[] { 1, 51m, true, "Столовая №1" });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "AvailableMoneyToOrder", "CanMultiSelect", "Name", "Position" },
                values: new object[] { 2, 0m, false, "ГлаголЪ", 1 });

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

            migrationBuilder.CreateIndex(
                name: "IX_DishCategories_SupplierId",
                table: "DishCategories",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_CategoryId",
                table: "DishItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_SupplierId",
                table: "DishItems",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DishItemsToWeekDays_WeekDayId",
                table: "DishItemsToWeekDays",
                column: "WeekDayId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DishItemId",
                table: "OrderItems",
                column: "DishItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishItemsToWeekDays");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "WeekDays");

            migrationBuilder.DropTable(
                name: "DishItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "DishCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
