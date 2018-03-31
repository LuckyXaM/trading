﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TrDeals.Data;
using TrDeals.Data.Models;

namespace TrDeals.Migrations
{
    [DbContext(typeof(TrDealsContext))]
    [Migration("20180331092930_OfferType")]
    partial class OfferType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("TrDeals.Data.Models.Offer", b =>
                {
                    b.Property<Guid>("OfferId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CurrencyFromId")
                        .IsRequired();

                    b.Property<string>("CurrencyToId")
                        .IsRequired();

                    b.Property<int>("OfferType");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("UserId");

                    b.Property<decimal>("Volume");

                    b.HasKey("OfferId");

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.HasIndex("Price");

                    b.HasIndex("UserId");

                    b.ToTable("Offers");
                });
#pragma warning restore 612, 618
        }
    }
}
