﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCommunalPayments.Data.Context;

namespace MyCommunalPayments.Data.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20200901151019_changeProviderType")]
    partial class changeProviderType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MyCommunalPayments.Models.Models.Invoice", b =>
                {
                    b.Property<int>("IdInvoice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("InvoiceSum")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Period")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Provider")
                        .HasColumnType("int");

                    b.HasKey("IdInvoice");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.InvoiceServices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("InvoiceIdInvoice")
                        .HasColumnType("int");

                    b.Property<int?>("ServiceIdService")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceIdInvoice");

                    b.HasIndex("ServiceIdService");

                    b.ToTable("InvoiceServices");
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.Payment", b =>
                {
                    b.Property<int>("IdPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePayment")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderPath")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("PaymentSum")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("IdPayment");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.Provider", b =>
                {
                    b.Property<int>("IdProvider")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NameProvider")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("WebSite")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("IdProvider");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.Service", b =>
                {
                    b.Property<int>("IdService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NameService")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("IdService");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.Transactions", b =>
                {
                    b.Property<int>("IdTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Invoice")
                        .HasColumnType("int");

                    b.Property<int>("Payment")
                        .HasColumnType("int");

                    b.Property<string>("Period")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("IdTransaction");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.InvoiceServices", b =>
                {
                    b.HasOne("MyCommunalPayments.Models.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceIdInvoice");

                    b.HasOne("MyCommunalPayments.Models.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceIdService");
                });
#pragma warning restore 612, 618
        }
    }
}
