using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class On091020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_IdPayment",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IdPayment",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderPath",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IdPayment",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "IdOrder",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdOrder",
                table: "Payments",
                column: "IdOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_IdOrder",
                table: "Payments",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_IdOrder",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdOrder",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "OrderPath",
                table: "Payments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPayment",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdPayment",
                table: "Orders",
                column: "IdPayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_IdPayment",
                table: "Orders",
                column: "IdPayment",
                principalTable: "Payments",
                principalColumn: "IdPayment",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
