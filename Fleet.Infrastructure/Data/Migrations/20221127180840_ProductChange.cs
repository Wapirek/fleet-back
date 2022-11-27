using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infrastructure.Data.Migrations
{
    public partial class ProductChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kierunek_transakcji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kierunek = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kierunek_transakcji", x => x.Id);
                })
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
                name: "produkty_źródło",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    place = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produkty_źródło", x => x.Id);
                    table.ForeignKey(
                        name: "FK_produkty_źródło_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profil_użytkownika",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stan_konta = table.Column<double>(type: "double", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profil_użytkownika", x => x.Id);
                    table.ForeignKey(
                        name: "FK_profil_użytkownika_konto_AccountId",
                        column: x => x.AccountId,
                        principalTable: "konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "transakcja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    data_transakcji = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    waluta = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zapłacono_łącznie = table.Column<double>(type: "double", nullable: false),
                    nazwa_transakcji = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    TransactionDirectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transakcja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transakcja_kierunek_transakcji_TransactionDirectionId",
                        column: x => x.TransactionDirectionId,
                        principalTable: "kierunek_transakcji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transakcja_konto_AccountId",
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
                    cena = table.Column<double>(type: "double", nullable: false),
                    jednostka = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CatalogId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    productPlaceId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_produkty_produkty_źródło_productPlaceId",
                        column: x => x.productPlaceId,
                        principalTable: "produkty_źródło",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transakcja_pozycje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ilość = table.Column<double>(type: "double", nullable: false),
                    zapłacono = table.Column<double>(type: "double", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transakcja_pozycje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transakcja_pozycje_produkty_ProductId",
                        column: x => x.ProductId,
                        principalTable: "produkty",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transakcja_pozycje_transakcja_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "transakcja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_produkty_productPlaceId",
                table: "produkty",
                column: "productPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_produkty_źródło_AccountId",
                table: "produkty_źródło",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_profil_użytkownika_AccountId",
                table: "profil_użytkownika",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_przepływy_pieniężne_AccountId",
                table: "przepływy_pieniężne",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_AccountId",
                table: "transakcja",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_TransactionDirectionId",
                table: "transakcja",
                column: "TransactionDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_pozycje_ProductId",
                table: "transakcja_pozycje",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_pozycje_TransactionId",
                table: "transakcja_pozycje",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profil_użytkownika");

            migrationBuilder.DropTable(
                name: "przepływy_pieniężne");

            migrationBuilder.DropTable(
                name: "transakcja_pozycje");

            migrationBuilder.DropTable(
                name: "produkty");

            migrationBuilder.DropTable(
                name: "transakcja");

            migrationBuilder.DropTable(
                name: "katalog_produktów");

            migrationBuilder.DropTable(
                name: "produkty_źródło");

            migrationBuilder.DropTable(
                name: "kierunek_transakcji");

            migrationBuilder.DropTable(
                name: "konto");
        }
    }
}
