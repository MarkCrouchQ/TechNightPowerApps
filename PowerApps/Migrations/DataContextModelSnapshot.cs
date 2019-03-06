﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerApps.Models;

namespace PowerApps.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PowerApps.Models.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<byte[]>("Picure")
                        .HasColumnType("image");

                    b.HasKey("Id");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("PowerApps.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PowerApps.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgentId");

                    b.Property<int?>("CustomerId");

                    b.Property<bool>("HasShipped");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PowerApps.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("image");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PowerApps.Models.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsPackaged");

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductOrders");
                });

            modelBuilder.Entity("PowerApps.Models.Order", b =>
                {
                    b.HasOne("PowerApps.Models.Agent", "Agent")
                        .WithMany("Orders")
                        .HasForeignKey("AgentId");

                    b.HasOne("PowerApps.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("PowerApps.Models.ProductOrder", b =>
                {
                    b.HasOne("PowerApps.Models.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderId");

                    b.HasOne("PowerApps.Models.Product", "Product")
                        .WithMany("ProductOrders")
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}