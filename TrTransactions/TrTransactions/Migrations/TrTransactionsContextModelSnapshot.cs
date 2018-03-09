﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TrTransactions.Data;
using TrTransactions.Data.Models;

namespace TrTransactions.Migrations
{
    [DbContext(typeof(TrTransactionsContext))]
    partial class TrTransactionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("TrTransactions.Data.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CurrencyId")
                        .IsRequired();

                    b.Property<int>("TransactionType");

                    b.Property<Guid>("UserId");

                    b.Property<decimal>("Volume");

                    b.HasKey("TransactionId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
