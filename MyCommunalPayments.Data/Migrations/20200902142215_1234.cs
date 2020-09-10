using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class _1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Periods_IdPeriod",
                table: "Invoices",
                column: "IdPeriod",
                principalTable: "Periods",
                principalColumn: "IdKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Periods_IdPeriod",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_IdPeriod",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IdPeriod",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Periods",
                table: "Invoices",
                newName: "Period"
                );

        }
    }
}
