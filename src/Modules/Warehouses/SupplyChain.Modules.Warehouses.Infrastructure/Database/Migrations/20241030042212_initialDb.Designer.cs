﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SupplyChain.Modules.Warehouses.Infrastructure.Database;

#nullable disable

namespace SupplyChain.Modules.Warehouses.Infrastructure.Database.Migrations
{
    [DbContext(typeof(WarehouseDBContext))]
    [Migration("20241030042212_initialDb")]
    partial class initialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("warehouse")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.BinLocations.BinLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CODE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uuid")
                        .HasColumnName("WAREHOUSE_ID");

                    b.HasKey("Id")
                        .HasName("PK_BIN_LOCATIONS");

                    b.HasIndex("Code")
                        .HasDatabaseName("IX_BIN_LOCATIONS_CODE");

                    b.HasIndex("WarehouseId")
                        .HasDatabaseName("IX_BIN_LOCATIONS_WAREHOUSE_ID");

                    b.ToTable("BIN_LOCATIONS", "warehouse");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.CatalogQuantity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<Guid>("BinLocationId")
                        .HasColumnType("uuid")
                        .HasColumnName("BIN_LOCATION_ID");

                    b.Property<Guid>("CatalogId")
                        .HasColumnType("uuid")
                        .HasColumnName("CATALOG_ID");

                    b.Property<decimal>("OnHand")
                        .HasColumnType("numeric")
                        .HasColumnName("ON_HAND");

                    b.Property<decimal>("Reserved")
                        .HasColumnType("numeric")
                        .HasColumnName("RESERVED");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("PK_CATALOG_QUANTITIES");

                    b.HasIndex("BinLocationId")
                        .HasDatabaseName("IX_CATALOG_QUANTITIES_BIN_LOCATION_ID");

                    b.HasIndex("CatalogId")
                        .HasDatabaseName("IX_CATALOG_QUANTITIES_CATALOG_ID");

                    b.ToTable("CATALOG_QUANTITIES", "warehouse");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.Catalogs.Catalog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<string>("CatalogId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CATALOG_ID");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CATEGORY");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DESCRIPTION");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("STATUS");

                    b.HasKey("Id")
                        .HasName("PK_CATALOGS");

                    b.HasIndex("CatalogId")
                        .HasDatabaseName("IX_CATALOGS_CATALOG_ID");

                    b.ToTable("CATALOGS", "warehouse");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.Warehouses.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CODE");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DESCRIPTION");

                    b.HasKey("Id")
                        .HasName("PK_WAREHOUSES");

                    b.HasIndex("Code")
                        .HasDatabaseName("IX_WAREHOUSES_CODE");

                    b.ToTable("WAREHOUSES", "warehouse");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.BinLocations.BinLocation", b =>
                {
                    b.HasOne("SupplyChain.Modules.Warehouses.Domain.Warehouses.Warehouse", "Warehouse")
                        .WithMany("BinLocations")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BIN_LOCATIONS_WAREHOUSES_WAREHOUSE_ID");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.CatalogQuantity", b =>
                {
                    b.HasOne("SupplyChain.Modules.Warehouses.Domain.BinLocations.BinLocation", "Location")
                        .WithMany("Quantities")
                        .HasForeignKey("BinLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CATALOG_QUANTITIES_BIN_LOCATIONS_BIN_LOCATION_ID");

                    b.HasOne("SupplyChain.Modules.Warehouses.Domain.Catalogs.Catalog", "Catalog")
                        .WithMany("Quantities")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CATALOG_QUANTITIES_CATALOGS_CATALOG_ID");

                    b.Navigation("Catalog");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.Warehouses.Warehouse", b =>
                {
                    b.OwnsOne("SupplyChain.Modules.Warehouses.Domain.Warehouses.WarehouseAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("WarehouseId")
                                .HasColumnType("uuid")
                                .HasColumnName("ID");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ADDRESS_CITY");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ADDRESS_COUNTRY");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ADDRESS_STREET");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ADDRESS_ZIP_CODE");

                            b1.HasKey("WarehouseId");

                            b1.ToTable("WAREHOUSES", "warehouse");

                            b1.WithOwner()
                                .HasForeignKey("WarehouseId")
                                .HasConstraintName("FK_WAREHOUSES_WAREHOUSES_ID");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.BinLocations.BinLocation", b =>
                {
                    b.Navigation("Quantities");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.Catalogs.Catalog", b =>
                {
                    b.Navigation("Quantities");
                });

            modelBuilder.Entity("SupplyChain.Modules.Warehouses.Domain.Warehouses.Warehouse", b =>
                {
                    b.Navigation("BinLocations");
                });
#pragma warning restore 612, 618
        }
    }
}