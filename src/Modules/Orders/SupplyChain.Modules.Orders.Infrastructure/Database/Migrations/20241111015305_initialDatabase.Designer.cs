﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SupplyChain.Modules.Orders.Infrastructure.Database.Data;

#nullable disable

namespace SupplyChain.Modules.Orders.Infrastructure.Database.Migrations
{
    [DbContext(typeof(OrdersDbContext))]
    [Migration("20241111015305_initialDatabase")]
    partial class initialDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("orders")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SupplyChain.Modules.Orders.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("CUSTOMER_ID");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uuid")
                        .HasColumnName("WAREHOUSE_ID");

                    b.HasKey("Id")
                        .HasName("PK_ORDERS");

                    b.ToTable("orders", "orders");
                });

            modelBuilder.Entity("SupplyChain.Modules.Orders.Domain.Orders.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<Guid>("CatalogId")
                        .HasColumnType("uuid")
                        .HasColumnName("CATALOG_ID");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("ORDER_ID");

                    b.Property<decimal>("OrderQuantity")
                        .HasColumnType("numeric")
                        .HasColumnName("ORDER_QUANTITY");

                    b.HasKey("Id")
                        .HasName("PK_ORDER_DETAILS");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("IX_ORDER_DETAILS_ORDER_ID");

                    b.ToTable("order_details", "orders");
                });

            modelBuilder.Entity("SupplyChain.Modules.Orders.Domain.Orders.OrderDetail", b =>
                {
                    b.HasOne("SupplyChain.Modules.Orders.Domain.Orders.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ORDER_DETAILS_ORDERS_ORDER_ID");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SupplyChain.Modules.Orders.Domain.Orders.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
