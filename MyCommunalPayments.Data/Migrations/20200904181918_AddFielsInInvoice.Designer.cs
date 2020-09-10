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
    [Migration("20200904181918_AddFielsInInvoice")]
    partial class AddFielsInInvoice
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

                    b.Property<int>("IdPeriod")
                        .HasColumnType("int");

                    b.Property<int>("IdProvider")
                        .HasColumnType("int");

                    b.Property<decimal>("InvoiceSum")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("Pay")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("IdInvoice");

                    b.HasIndex("IdPeriod");

                    b.HasIndex("IdProvider");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.InvoiceServices", b =>
                {
                    b.Property<int>("IdInvoiceServices")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("IdInvoice")
                        .HasColumnType("int");

                    b.Property<int>("IdService")
                        .HasColumnType("int");

                    b.HasKey("IdInvoiceServices");

                    b.HasIndex("IdInvoice");

                    b.HasIndex("IdService");

                    b.ToTable("InvoceServices");
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

            modelBuilder.Entity("MyCommunalPayments.Models.Models.Period", b =>
                {
                    b.Property<int>("IdKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Month")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Year")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("IdKey");

                    b.ToTable("Periods");
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

                    b.Property<bool>("IsCounter")
                        .HasColumnType("tinyint(1)");

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

            modelBuilder.Entity("MyCommunalPayments.Models.Models.Invoice", b =>
                {
                    b.HasOne("MyCommunalPayments.Models.Models.Period", "Period")
                        .WithMany()
                        .HasForeignKey("IdPeriod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyCommunalPayments.Models.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("IdProvider")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyCommunalPayments.Models.Models.InvoiceServices", b =>
                {
                    b.HasOne("MyCommunalPayments.Models.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("IdInvoice")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyCommunalPayments.Models.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("IdService")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
