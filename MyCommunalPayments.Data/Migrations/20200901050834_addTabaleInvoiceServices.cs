using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class addTabaleInvoiceServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceIdInvoice = table.Column<int>(nullable: true),
                    ServiceIdService = table.Column<int>(nullable: true),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceServices_Invoices_InvoiceIdInvoice",
                        column: x => x.InvoiceIdInvoice,
                        principalTable: "Invoices",
                        principalColumn: "IdInvoice",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceServices_Services_ServiceIdService",
                        column: x => x.ServiceIdService,
                        principalTable: "Services",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServices_InvoiceIdInvoice",
                table: "InvoiceServices",
                column: "InvoiceIdInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServices_ServiceIdService",
                table: "InvoiceServices",
                column: "ServiceIdService");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceServices");
        }
    }
}
