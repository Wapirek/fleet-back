using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infrastructure.Data.Migrations
{
    public partial class BaseInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "konto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    login = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salt = table.Column<byte[]>(type: "longblob", nullable: false),
                    hash = table.Column<byte[]>(type: "longblob", nullable: false),
                    utworzono = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_konto", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "katalog_produktów",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nazwa = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_katalog_produktów", x => x.Id);
                    table.ForeignKey(
                        name: "FK_katalog_produktów_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produkty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nazwa_produktu = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sprzedawca = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cena = table.Column<double>(type: "double", nullable: false),
                    jednostka = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CatalogId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produkty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_produkty_katalog_produktów_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "katalog_produktów",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_produkty_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transakcja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ilość = table.Column<double>(type: "double", nullable: false),
                    data_transakcji = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    waluta = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zapłacono = table.Column<double>(type: "double", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transakcja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transakcja_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transakcja_produkty_ProductId",
                        column: x => x.ProductId,
                        principalTable: "produkty",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_katalog_produktów_AccountId",
                table: "katalog_produktów",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_produkty_AccountId",
                table: "produkty",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_produkty_CatalogId",
                table: "produkty",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_AccountId",
                table: "transakcja",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_ProductId",
                table: "transakcja",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transakcja");

            migrationBuilder.DropTable(
                name: "produkty");

            migrationBuilder.DropTable(
                name: "katalog_produktów");

            migrationBuilder.DropTable(
                name: "konto");
        }
    }
}
