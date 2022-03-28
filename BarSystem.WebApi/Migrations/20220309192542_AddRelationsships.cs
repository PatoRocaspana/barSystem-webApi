using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarSystem.WebApi.Migrations
{
    public partial class AddRelationsships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Tables_TableId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Tables_TableId",
                table: "Drinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Employees_WaiterId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_TableId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_TableId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "WaiterId",
                table: "Tables",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables",
                newName: "IX_Tables_EmployeeId");

            migrationBuilder.CreateTable(
                name: "DishTable",
                columns: table => new
                {
                    DishesId = table.Column<int>(type: "int", nullable: false),
                    TablesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishTable", x => new { x.DishesId, x.TablesId });
                    table.ForeignKey(
                        name: "FK_DishTable_Dishes_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishTable_Tables_TablesId",
                        column: x => x.TablesId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrinkTable",
                columns: table => new
                {
                    DrinksId = table.Column<int>(type: "int", nullable: false),
                    TablesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkTable", x => new { x.DrinksId, x.TablesId });
                    table.ForeignKey(
                        name: "FK_DrinkTable_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkTable_Tables_TablesId",
                        column: x => x.TablesId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishTable_TablesId",
                table: "DishTable",
                column: "TablesId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkTable_TablesId",
                table: "DrinkTable",
                column: "TablesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Employees_EmployeeId",
                table: "Tables",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Employees_EmployeeId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "DishTable");

            migrationBuilder.DropTable(
                name: "DrinkTable");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Tables",
                newName: "WaiterId");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_EmployeeId",
                table: "Tables",
                newName: "IX_Tables_WaiterId");

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Drinks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_TableId",
                table: "Drinks",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_TableId",
                table: "Dishes",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Tables_TableId",
                table: "Dishes",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Tables_TableId",
                table: "Drinks",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Employees_WaiterId",
                table: "Tables",
                column: "WaiterId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
