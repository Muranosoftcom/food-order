using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOrder.Persistence.Migrations
{
    public partial class fix_connection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryKey",
                table: "DishItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierKey",
                table: "DishItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DishCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_CategoryKey",
                table: "DishItems",
                column: "CategoryKey");

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_SupplierKey",
                table: "DishItems",
                column: "SupplierKey");

            migrationBuilder.AddForeignKey(
                name: "FK_DishItems_DishCategory_CategoryKey",
                table: "DishItems",
                column: "CategoryKey",
                principalTable: "DishCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishItems_Supplier_SupplierKey",
                table: "DishItems",
                column: "SupplierKey",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishItems_DishCategory_CategoryKey",
                table: "DishItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DishItems_Supplier_SupplierKey",
                table: "DishItems");

            migrationBuilder.DropTable(
                name: "DishCategory");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_DishItems_CategoryKey",
                table: "DishItems");

            migrationBuilder.DropIndex(
                name: "IX_DishItems_SupplierKey",
                table: "DishItems");

            migrationBuilder.DropColumn(
                name: "CategoryKey",
                table: "DishItems");

            migrationBuilder.DropColumn(
                name: "SupplierKey",
                table: "DishItems");
        }
    }
}
