using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timbangan.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clienttype",
                columns: table => new
                {
                    ClientTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clienttype", x => x.ClientTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "penugasan",
                columns: table => new
                {
                    PenugasanID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaPenugasan = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_penugasan", x => x.PenugasanID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roda",
                columns: table => new
                {
                    RodaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JumlahRoda = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roda", x => x.RodaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.StatusID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipekendaraan",
                columns: table => new
                {
                    TipeKendaraanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Kode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaTipe = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipekendaraan", x => x.TipeKendaraanID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AreaKerjas",
                columns: table => new
                {
                    AreaKerjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaArea = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PenugasanID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaKerjas", x => x.AreaKerjaID);
                    table.ForeignKey(
                        name: "FK_AreaKerjas_penugasan_PenugasanID",
                        column: x => x.PenugasanID,
                        principalTable: "penugasan",
                        principalColumn: "PenugasanID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_clients_status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "status",
                        principalColumn: "StatusID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kendaraan",
                columns: table => new
                {
                    KendaraanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UniqueID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    NoPolisi = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoPintu = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RFID = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    AreaKerjaID = table.Column<int>(type: "int", nullable: false),
                    TipeKendaraanID = table.Column<int>(type: "int", nullable: false),
                    RodaID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    AvgMasuk = table.Column<int>(type: "int", nullable: true),
                    AvgKeluar = table.Column<int>(type: "int", nullable: true),
                    BeratKIR = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kendaraan", x => x.KendaraanID);
                    table.ForeignKey(
                        name: "FK_kendaraan_AreaKerjas_AreaKerjaID",
                        column: x => x.AreaKerjaID,
                        principalTable: "AreaKerjas",
                        principalColumn: "AreaKerjaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kendaraan_clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kendaraan_roda_RodaID",
                        column: x => x.RodaID,
                        principalTable: "roda",
                        principalColumn: "RodaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kendaraan_status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_kendaraan_tipekendaraan_TipeKendaraanID",
                        column: x => x.TipeKendaraanID,
                        principalTable: "tipekendaraan",
                        principalColumn: "TipeKendaraanID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AreaKerjas_PenugasanID",
                table: "AreaKerjas",
                column: "PenugasanID");

            migrationBuilder.CreateIndex(
                name: "IX_clients_StatusID",
                table: "clients",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_kendaraan_AreaKerjaID",
                table: "kendaraan",
                column: "AreaKerjaID");

            migrationBuilder.CreateIndex(
                name: "IX_kendaraan_ClientID",
                table: "kendaraan",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_kendaraan_RodaID",
                table: "kendaraan",
                column: "RodaID");

            migrationBuilder.CreateIndex(
                name: "IX_kendaraan_StatusID",
                table: "kendaraan",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_kendaraan_TipeKendaraanID",
                table: "kendaraan",
                column: "TipeKendaraanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clienttype");

            migrationBuilder.DropTable(
                name: "kendaraan");

            migrationBuilder.DropTable(
                name: "AreaKerjas");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "roda");

            migrationBuilder.DropTable(
                name: "tipekendaraan");

            migrationBuilder.DropTable(
                name: "penugasan");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
