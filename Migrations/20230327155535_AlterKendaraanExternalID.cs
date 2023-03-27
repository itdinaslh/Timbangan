using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timbangan.Migrations
{
    public partial class AlterKendaraanExternalID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalID",
                table: "kendaraan",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<bool>(
                name: "IsPasar",
                table: "kendaraan",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalID",
                table: "kendaraan");

            migrationBuilder.DropColumn(
                name: "IsPasar",
                table: "kendaraan");
        }
    }
}
