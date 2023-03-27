using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timbangan.Migrations
{
    public partial class AlterTransactionForKendaraan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_KendaraanID",
                table: "Transactions",
                column: "KendaraanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_kendaraan_KendaraanID",
                table: "Transactions",
                column: "KendaraanID",
                principalTable: "kendaraan",
                principalColumn: "KendaraanID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_kendaraan_KendaraanID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_KendaraanID",
                table: "Transactions");
        }
    }
}
