﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TrDeals.Data;

namespace TrDeals.Migrations
{
    [DbContext(typeof(TrDealsContext))]
    partial class TrDealsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("TrDeals.Data.Models.Offer", b =>
                {
                    b.Property<Guid>("OfferId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Ammount");

                    b.Property<decimal>("Course");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CurrencyFromId")
                        .IsRequired();

                    b.Property<string>("CurrencyToId")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("OfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Offers");
                });
#pragma warning restore 612, 618
        }
    }
}
