using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class AddTableInvoiceServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoceServices",
                columns: table => new
                {
                    IdInvoiceServices = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInvoice = table.Column<int>(nullable: false),
                    IdService = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoceServices", x => x.IdInvoiceServices);
                    table.ForeignKey(
                        name: "FK_InvoceServices_Invoices_IdInvoice",
                        column: x => x.IdInvoice,
                        principalTable: "Invoices",
                        principalColumn: "IdInvoice",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoceServices_Services_IdService",
                        column: x => x.IdService,
                        principalTable: "Services",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoceServices_IdInvoice",
                table: "InvoceServices",
                column: "IdInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_InvoceServices_IdService",
                table: "InvoceServices",
                column: "IdService");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoceServices");
        }
    }
}
