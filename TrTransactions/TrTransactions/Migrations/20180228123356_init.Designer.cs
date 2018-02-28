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
    [Migration("20180228123356_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("TrTransactions.Data.Models.CurrencyType", b =>
                {
                    b.Property<string>("TransactionTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("TransactionTypeId");

                    b.ToTable("CurrencyTypes");
                });

            modelBuilder.Entity("TrTransactions.Data.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Ammount");

                    b.Property<Guid?>("AskId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CurrencyTypeId")
                        .IsRequired();

                    b.Property<int>("TransactionType");

                    b.Property<Guid>("UserId");

                    b.HasKey("TransactionId");

                    b.HasIndex("CurrencyTypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TrTransactions.Data.Models.Transaction", b =>
                {
                    b.HasOne("TrTransactions.Data.Models.CurrencyType", "CurrencyType")
                        .WithMany()
                        .HasForeignKey("CurrencyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}