using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCommunalPayments.Data.Migrations
{
    public partial class AddTableProvidersServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProvidersServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProvider = table.Column<int>(nullable: false),
                    IdService = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidersServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvidersServices_Providers_IdProvider",
                        column: x => x.IdProvider,
                        principalTable: "Providers",
                        principalColumn: "IdProvider",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvidersServices_Services_IdService",
                        column: x => x.IdService,
                        principalTable: "Services",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvidersServices_IdProvider",
                table: "ProvidersServices",
                column: "IdProvider");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidersServices_IdService",
                table: "ProvidersServices",
                column: "IdService");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvidersServices");
        }
    }
}
