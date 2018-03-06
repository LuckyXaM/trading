using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TrCurrencies.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyPairs",
                columns: table => new
                {
                    CurrencyPairId = table.Column<Guid>(nullable: false),
                    CurrencyPairFromId = table.Column<string>(nullable: false),
                    CurrencyPairToId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyPairs", x => x.CurrencyPairId);
                    table.ForeignKey(
                        name: "FK_CurrencyPairs_Currencies_CurrencyPairFromId",
                        column: x => x.CurrencyPairFromId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyPairs_Currencies_CurrencyPairToId",
                        column: x => x.CurrencyPairToId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPairs_CurrencyPairFromId",
                table: "CurrencyPairs",
                column: "CurrencyPairFromId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPairs_CurrencyPairToId",
                table: "CurrencyPairs",
                column: "CurrencyPairToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyPairs");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
