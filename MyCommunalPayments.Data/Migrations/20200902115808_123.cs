using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IdProvider",
                table: "Invoices",
                column: "IdProvider");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Providers_IdProvider",
                table: "Invoices",
                column: "IdProvider",
                principalTable: "Providers",
                principalColumn: "IdProvider",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Providers_IdProvider",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_IdProvider",
                table: "Invoices");

            
        }
    }
}
