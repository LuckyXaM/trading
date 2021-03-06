﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TrDeals.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CurrencyFromId = table.Column<string>(nullable: false),
                    CurrencyToId = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CurrencyFromId",
                table: "Offers",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CurrencyToId",
                table: "Offers",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_Price",
                table: "Offers",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");
        }
    }
}
