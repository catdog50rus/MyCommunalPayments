using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class AddPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdInvoice",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Payments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdInvoice",
                table: "Payments",
                column: "IdInvoice");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Invoices_IdInvoice",
                table: "Payments",
                column: "IdInvoice",
                principalTable: "Invoices",
                principalColumn: "IdInvoice",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_IdInvoice",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdInvoice",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IdInvoice",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Payments");
        }
    }
}
