﻿// <auto-generated />
using System;
using Fleet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fleet.Infrastructure.Data.Migrations
{
    [DbContext(typeof(FleetContext))]
    [Migration("20221127180840_ProductChange")]
    partial class ProductChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Fleet.Core.Entities.AccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("utworzono");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("email");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("hash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("salt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("login");

                    b.HasKey("Id");

                    b.ToTable("konto");
                });

            modelBuilder.Entity("Fleet.Core.Entities.CashFlowEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CashFlowKind")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("rodzaj_przepływu");

                    b.Property<double>("Charge")
                        .HasColumnType("double")
                        .HasColumnName("obciążenie");

                    b.Property<DateTime>("NextCashFlow")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("kolejny_przepływ");

                    b.Property<int>("PeriodicityDay")
                        .HasColumnType("int")
                        .HasColumnName("cykliczność_dni");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Źródło");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("przepływy_pieniężne");
                });

            modelBuilder.Entity("Fleet.Core.Entities.CatalogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CatalogName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("nazwa");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("katalog_produktów");
                });

            modelBuilder.Entity("Fleet.Core.Entities.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("CatalogId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double")
                        .HasColumnName("cena");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nazwa_produktu");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("jednostka");

                    b.Property<int>("productPlaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CatalogId");

                    b.HasIndex("productPlaceId");

                    b.ToTable("produkty");
                });

            modelBuilder.Entity("Fleet.Core.Entities.ProductPlaceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("place");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("produkty_źródło");
                });

            modelBuilder.Entity("Fleet.Core.Entities.TransactionDirectionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TransactionDirection")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("kierunek");

                    b.HasKey("Id");

                    b.ToTable("kierunek_transakcji");
                });

            modelBuilder.Entity("Fleet.Core.Entities.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("waluta");

                    b.Property<double>("TotalPaid")
                        .HasColumnType("double")
                        .HasColumnName("zapłacono_łącznie");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("data_transakcji");

                    b.Property<int>("TransactionDirectionId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("nazwa_transakcji");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionDirectionId");

                    b.ToTable("transakcja");
                });

            modelBuilder.Entity("Fleet.Core.Entities.TransactionPositionsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Paid")
                        .HasColumnType("double")
                        .HasColumnName("zapłacono");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("double")
                        .HasColumnName("ilość");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactionId");

                    b.ToTable("transakcja_pozycje");
                });

            modelBuilder.Entity("Fleet.Core.Entities.UserProfileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("AccountBalance")
                        .HasColumnType("double")
                        .HasColumnName("stan_konta");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("profil_użytkownika");
                });

            modelBuilder.Entity("Fleet.Core.Entities.CashFlowEntity", b =>
                {
                    b.HasOne("Fleet.Core.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Fleet.Core.Entities.CatalogEntity", b =>
                {
                    b.HasOne("Fleet.Core.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Fleet.Core.Entities.ProductEntity", b =>
                {
                    b.HasOne("Fleet.Core.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fleet.Core.Entities.CatalogEntity", "Catalog")
                        .WithMany("Produts")
                        .HasForeignKey("CatalogId");

                    b.HasOne("Fleet.Core.Entities.ProductPlaceEntity", "ProductPlace")
                        .WithMany()
                        .HasForeignKey("productPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Catalog");

                    b.Navigation("ProductPlace");
                });

            modelBuilder.Entity("Fleet.Core.Entities.ProductPlaceEntity", b =>
                {
                    b.HasOne("Fleet.Core.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Fleet.Core.Entities.TransactionEntity", b =>
                {
                    b.HasOne("Fleet.Core.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fleet.Core.Entities.TransactionDirectionEntity", "TransactionDirection")
                        .WithMany()
                        .HasForeignKey("TransactionDirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("TransactionDirection");
                });

            modelBuilder.Entity("Fleet.Core.Entities.TransactionPositionsEntity", b =>
                {
                    b.HasOne("Fleet.Core.Entities.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("Fleet.Core.Entities.TransactionEntity", "Transaction")
                        .WithMany("TransactionPostions")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Fleet.Core.Entities.UserProfileEntity", b =>
                {
                    b.HasOne("Fleet.Core.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Fleet.Core.Entities.CatalogEntity", b =>
                {
                    b.Navigation("Produts");
                });

            modelBuilder.Entity("Fleet.Core.Entities.TransactionEntity", b =>
                {
                    b.Navigation("TransactionPostions");
                });
#pragma warning restore 612, 618
        }
    }
}
