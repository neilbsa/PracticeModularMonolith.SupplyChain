using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Modules.Warehouses.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "warehouse");

            migrationBuilder.CreateTable(
                name: "CATALOGS",
                schema: "warehouse",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CATALOG_ID = table.Column<string>(type: "text", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "text", nullable: false),
                    CATEGORY = table.Column<string>(type: "text", nullable: false),
                    STATUS = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATALOGS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WAREHOUSES",
                schema: "warehouse",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CODE = table.Column<string>(type: "text", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "text", nullable: false),
                    ADDRESS_STREET = table.Column<string>(type: "text", nullable: false),
                    ADDRESS_CITY = table.Column<string>(type: "text", nullable: false),
                    ADDRESS_ZIP_CODE = table.Column<string>(type: "text", nullable: false),
                    ADDRESS_COUNTRY = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAREHOUSES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BIN_LOCATIONS",
                schema: "warehouse",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    WAREHOUSE_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CODE = table.Column<string>(type: "text", nullable: false),
                    NAME = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIN_LOCATIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BIN_LOCATIONS_WAREHOUSES_WAREHOUSE_ID",
                        column: x => x.WAREHOUSE_ID,
                        principalSchema: "warehouse",
                        principalTable: "WAREHOUSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CATALOG_QUANTITIES",
                schema: "warehouse",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    BIN_LOCATION_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CATALOG_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ON_HAND = table.Column<decimal>(type: "numeric", nullable: false),
                    RESERVED = table.Column<decimal>(type: "numeric", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATALOG_QUANTITIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CATALOG_QUANTITIES_BIN_LOCATIONS_BIN_LOCATION_ID",
                        column: x => x.BIN_LOCATION_ID,
                        principalSchema: "warehouse",
                        principalTable: "BIN_LOCATIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CATALOG_QUANTITIES_CATALOGS_CATALOG_ID",
                        column: x => x.CATALOG_ID,
                        principalSchema: "warehouse",
                        principalTable: "CATALOGS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BIN_LOCATIONS_CODE",
                schema: "warehouse",
                table: "BIN_LOCATIONS",
                column: "CODE");

            migrationBuilder.CreateIndex(
                name: "IX_BIN_LOCATIONS_WAREHOUSE_ID",
                schema: "warehouse",
                table: "BIN_LOCATIONS",
                column: "WAREHOUSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATALOG_QUANTITIES_BIN_LOCATION_ID",
                schema: "warehouse",
                table: "CATALOG_QUANTITIES",
                column: "BIN_LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATALOG_QUANTITIES_CATALOG_ID",
                schema: "warehouse",
                table: "CATALOG_QUANTITIES",
                column: "CATALOG_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATALOGS_CATALOG_ID",
                schema: "warehouse",
                table: "CATALOGS",
                column: "CATALOG_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WAREHOUSES_CODE",
                schema: "warehouse",
                table: "WAREHOUSES",
                column: "CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CATALOG_QUANTITIES",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "BIN_LOCATIONS",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "CATALOGS",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "WAREHOUSES",
                schema: "warehouse");
        }
    }
}
