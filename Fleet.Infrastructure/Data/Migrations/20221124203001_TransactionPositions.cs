using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infrastructure.Data.Migrations
{
    public partial class TransactionPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transakcja_produkty_ProductId",
                table: "transakcja");

            migrationBuilder.DropTable(
                name: "opłaty_użytkownika");

            migrationBuilder.DropTable(
                name: "przychody_użytkownika");

            migrationBuilder.DropIndex(
                name: "IX_transakcja_ProductId",
                table: "transakcja");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "transakcja");

            migrationBuilder.DropColumn(
                name: "ilość",
                table: "transakcja");

            migrationBuilder.DropColumn(
                name: "sprzedawca",
                table: "produkty");

            migrationBuilder.RenameColumn(
                name: "zapłacono",
                table: "transakcja",
                newName: "zapłacono_łącznie");

            migrationBuilder.AddColumn<int>(
                name: "TransactionDirectionId",
                table: "transakcja",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "nazwa_transakcji",
                table: "transakcja",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TransactionPostionsEntityId",
                table: "produkty",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productPlaceId",
                table: "produkty",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "product_place",
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
                    table.PrimaryKey("PK_product_place", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_place_konto_AccountId",
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
                        name: "FK_transakcja_pozycje_transakcja_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "transakcja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_TransactionDirectionId",
                table: "transakcja",
                column: "TransactionDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_produkty_productPlaceId",
                table: "produkty",
                column: "productPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_produkty_TransactionPostionsEntityId",
                table: "produkty",
                column: "TransactionPostionsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_product_place_AccountId",
                table: "product_place",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_przepływy_pieniężne_AccountId",
                table: "przepływy_pieniężne",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transakcja_pozycje_TransactionId",
                table: "transakcja_pozycje",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_produkty_product_place_productPlaceId",
                table: "produkty",
                column: "productPlaceId",
                principalTable: "product_place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produkty_transakcja_pozycje_TransactionPostionsEntityId",
                table: "produkty",
                column: "TransactionPostionsEntityId",
                principalTable: "transakcja_pozycje",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transakcja_kierunek_transakcji_TransactionDirectionId",
                table: "transakcja",
                column: "TransactionDirectionId",
                principalTable: "kierunek_transakcji",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produkty_product_place_productPlaceId",
                table: "produkty");

            migrationBuilder.DropForeignKey(
                name: "FK_produkty_transakcja_pozycje_TransactionPostionsEntityId",
                table: "produkty");

            migrationBuilder.DropForeignKey(
                name: "FK_transakcja_kierunek_transakcji_TransactionDirectionId",
                table: "transakcja");

            migrationBuilder.DropTable(
                name: "kierunek_transakcji");

            migrationBuilder.DropTable(
                name: "product_place");

            migrationBuilder.DropTable(
                name: "przepływy_pieniężne");

            migrationBuilder.DropTable(
                name: "transakcja_pozycje");

            migrationBuilder.DropIndex(
                name: "IX_transakcja_TransactionDirectionId",
                table: "transakcja");

            migrationBuilder.DropIndex(
                name: "IX_produkty_productPlaceId",
                table: "produkty");

            migrationBuilder.DropIndex(
                name: "IX_produkty_TransactionPostionsEntityId",
                table: "produkty");

            migrationBuilder.DropColumn(
                name: "TransactionDirectionId",
                table: "transakcja");

            migrationBuilder.DropColumn(
                name: "nazwa_transakcji",
                table: "transakcja");

            migrationBuilder.DropColumn(
                name: "TransactionPostionsEntityId",
                table: "produkty");

            migrationBuilder.DropColumn(
                name: "productPlaceId",
                table: "produkty");

            migrationBuilder.RenameColumn(
                name: "zapłacono_łącznie",
                table: "transakcja",
                newName: "zapłacono");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "transakcja",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ilość",
                table: "transakcja",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "sprzedawca",
                table: "produkty",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_transakcja_ProductId",
                table: "transakcja",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_opłaty_użytkownika_AccountId",
                table: "opłaty_użytkownika",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_przychody_użytkownika_AccountId",
                table: "przychody_użytkownika",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_transakcja_produkty_ProductId",
                table: "transakcja",
                column: "ProductId",
                principalTable: "produkty",
                principalColumn: "Id");
        }
    }
}
