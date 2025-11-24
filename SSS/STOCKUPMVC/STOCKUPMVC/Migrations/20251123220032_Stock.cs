using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STOCKUPMVC.Migrations
{
    /// <inheritdoc />
    public partial class Stock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_AspNetUsers_CreatedById",
                table: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_CreatedById",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "StockMovements");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "StockMovements",
                newName: "MovementTime");

            migrationBuilder.RenameColumn(
                name: "MovementID",
                table: "StockMovements",
                newName: "StockMovementID");

            migrationBuilder.AlterColumn<int>(
                name: "ToWarehouseID",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromWarehouseID",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "StockMovements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ApplicationUserId",
                table: "StockMovements",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_AspNetUsers_ApplicationUserId",
                table: "StockMovements",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_AspNetUsers_ApplicationUserId",
                table: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_ApplicationUserId",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "StockMovements");

            migrationBuilder.RenameColumn(
                name: "MovementTime",
                table: "StockMovements",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "StockMovementID",
                table: "StockMovements",
                newName: "MovementID");

            migrationBuilder.AlterColumn<int>(
                name: "ToWarehouseID",
                table: "StockMovements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FromWarehouseID",
                table: "StockMovements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MovementType",
                table: "StockMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_CreatedById",
                table: "StockMovements",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_AspNetUsers_CreatedById",
                table: "StockMovements",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
