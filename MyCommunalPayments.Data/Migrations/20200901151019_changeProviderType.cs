using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class changeProviderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Providers_ProviderIdProvider",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Invoices_InvoiceIdInvoice",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Payments_PaymentIdPayment",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_InvoiceIdInvoice",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PaymentIdPayment",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ProviderIdProvider",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceIdInvoice",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentIdPayment",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProviderIdProvider",
                table: "Invoices");

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "Invoice",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Payment",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Provider",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Invoices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "Transactions",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceIdInvoice",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentIdPayment",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProviderIdProvider",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InvoiceIdInvoice",
                table: "Transactions",
                column: "InvoiceIdInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentIdPayment",
                table: "Transactions",
                column: "PaymentIdPayment");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProviderIdProvider",
                table: "Invoices",
                column: "ProviderIdProvider");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Providers_ProviderIdProvider",
                table: "Invoices",
                column: "ProviderIdProvider",
                principalTable: "Providers",
                principalColumn: "IdProvider",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Invoices_InvoiceIdInvoice",
                table: "Transactions",
                column: "InvoiceIdInvoice",
                principalTable: "Invoices",
                principalColumn: "IdInvoice",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Payments_PaymentIdPayment",
                table: "Transactions",
                column: "PaymentIdPayment",
                principalTable: "Payments",
                principalColumn: "IdPayment",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
