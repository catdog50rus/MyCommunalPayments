using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class InitMySQLAddTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Invoices_InvoiceIdInvoice",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_InvoiceIdInvoice",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "InvoiceIdInvoice",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceIdInvoice",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_InvoiceIdInvoice",
                table: "Services",
                column: "InvoiceIdInvoice");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Invoices_InvoiceIdInvoice",
                table: "Services",
                column: "InvoiceIdInvoice",
                principalTable: "Invoices",
                principalColumn: "IdInvoice",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
