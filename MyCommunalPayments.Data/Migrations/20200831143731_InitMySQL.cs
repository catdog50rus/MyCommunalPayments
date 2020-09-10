using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class InitMySQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    IdPayment = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DatePayment = table.Column<DateTime>(nullable: false),
                    PaymentSum = table.Column<decimal>(nullable: false),
                    OrderPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.IdPayment);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    IdProvider = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameProvider = table.Column<string>(nullable: true),
                    WebSite = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.IdProvider);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    IdInvoice = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Period = table.Column<DateTime>(nullable: false),
                    ProviderIdProvider = table.Column<int>(nullable: true),
                    InvoiceSum = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.IdInvoice);
                    table.ForeignKey(
                        name: "FK_Invoices_Providers_ProviderIdProvider",
                        column: x => x.ProviderIdProvider,
                        principalTable: "Providers",
                        principalColumn: "IdProvider",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    IdService = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameService = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    InvoiceIdInvoice = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.IdService);
                    table.ForeignKey(
                        name: "FK_Services_Invoices_InvoiceIdInvoice",
                        column: x => x.InvoiceIdInvoice,
                        principalTable: "Invoices",
                        principalColumn: "IdInvoice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    IdTransaction = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Period = table.Column<DateTime>(nullable: false),
                    InvoiceIdInvoice = table.Column<int>(nullable: true),
                    PaymentIdPayment = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.IdTransaction);
                    table.ForeignKey(
                        name: "FK_Transactions_Invoices_InvoiceIdInvoice",
                        column: x => x.InvoiceIdInvoice,
                        principalTable: "Invoices",
                        principalColumn: "IdInvoice",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Payments_PaymentIdPayment",
                        column: x => x.PaymentIdPayment,
                        principalTable: "Payments",
                        principalColumn: "IdPayment",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProviderIdProvider",
                table: "Invoices",
                column: "ProviderIdProvider");

            migrationBuilder.CreateIndex(
                name: "IX_Services_InvoiceIdInvoice",
                table: "Services",
                column: "InvoiceIdInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InvoiceIdInvoice",
                table: "Transactions",
                column: "InvoiceIdInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentIdPayment",
                table: "Transactions",
                column: "PaymentIdPayment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
