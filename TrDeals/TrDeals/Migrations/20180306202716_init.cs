using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TrDeals.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asks",
                columns: table => new
                {
                    AskId = table.Column<Guid>(nullable: false),
                    Ammount = table.Column<decimal>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CurrencyFromId = table.Column<string>(nullable: false),
                    CurrencyToId = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asks", x => x.AskId);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    AskId = table.Column<Guid>(nullable: false),
                    Ammount = table.Column<decimal>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CurrencyFromId = table.Column<string>(nullable: false),
                    CurrencyToId = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.AskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asks");

            migrationBuilder.DropTable(
                name: "Bids");
        }
    }
}
