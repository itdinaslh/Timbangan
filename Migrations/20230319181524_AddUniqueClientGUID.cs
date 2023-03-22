using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timbangan.Migrations
{
    public partial class AddUniqueClientGUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaKerjas_penugasan_PenugasanID",
                table: "AreaKerjas");

            migrationBuilder.AddColumn<Guid>(
                name: "PkmID",
                table: "clients",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "PenugasanID",
                table: "AreaKerjas",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaKerjas_penugasan_PenugasanID",
                table: "AreaKerjas",
                column: "PenugasanID",
                principalTable: "penugasan",
                principalColumn: "PenugasanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaKerjas_penugasan_PenugasanID",
                table: "AreaKerjas");

            migrationBuilder.DropColumn(
                name: "PkmID",
                table: "clients");

            migrationBuilder.UpdateData(
                table: "AreaKerjas",
                keyColumn: "PenugasanID",
                keyValue: null,
                column: "PenugasanID",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PenugasanID",
                table: "AreaKerjas",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaKerjas_penugasan_PenugasanID",
                table: "AreaKerjas",
                column: "PenugasanID",
                principalTable: "penugasan",
                principalColumn: "PenugasanID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
