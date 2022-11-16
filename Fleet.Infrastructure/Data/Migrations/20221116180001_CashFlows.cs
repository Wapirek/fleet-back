using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infrastructure.Data.Migrations
{
    public partial class CashFlows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "opłaty_użytkownika");

            migrationBuilder.DropTable(
                name: "przychody_użytkownika");

            migrationBuilder.CreateTable(
                name: "przepływy_pieniężne",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    obciążenie = table.Column<double>(type: "double", nullable: false),
                    Źródło = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cykliczność_dni = table.Column<int>(type: "int", nullable: false),
                    kolejny_przepływ = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    rodzaj_przepływu = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przepływy_pieniężne", x => x.Id);
                    table.ForeignKey(
                        name: "FK_przepływy_pieniężne_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_przepływy_pieniężne_AccountId",
                table: "przepływy_pieniężne",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "przepływy_pieniężne");

            migrationBuilder.CreateTable(
                name: "opłaty_użytkownika",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    kolejna_płatność = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    kwota_płatności = table.Column<double>(type: "double", nullable: false),
                    cykliczność_dni = table.Column<int>(type: "int", nullable: false),
                    Źródło = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opłaty_użytkownika", x => x.Id);
                    table.ForeignKey(
                        name: "FK_opłaty_użytkownika_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "przychody_użytkownika",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    kwota_przychodu = table.Column<double>(type: "double", nullable: false),
                    kolejny_przychód = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cykliczność_dni = table.Column<int>(type: "int", nullable: false),
                    Źródło = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przychody_użytkownika", x => x.Id);
                    table.ForeignKey(
                        name: "FK_przychody_użytkownika_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_opłaty_użytkownika_AccountId",
                table: "opłaty_użytkownika",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_przychody_użytkownika_AccountId",
                table: "przychody_użytkownika",
                column: "AccountId");
        }
    }
}
