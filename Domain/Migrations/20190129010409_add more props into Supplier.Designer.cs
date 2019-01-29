﻿// <auto-generated />
using System;
using Domain.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(FoodOrderContext))]
    [Migration("20190129010409_add more props into Supplier")]
    partial class addmorepropsintoSupplier
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.DishItemToWeekDay", b =>
                {
                    b.Property<int>("DishItemId");

                    b.Property<int>("WeekDayId");

                    b.HasKey("DishItemId", "WeekDayId");

                    b.HasIndex("WeekDayId");

                    b.ToTable("DishItemsToWeekDays");
                });

            modelBuilder.Entity("Domain.Entities.DishCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("DishCategories");
                });

            modelBuilder.Entity("Domain.Entities.DishItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AvailableUntil");

                    b.Property<int>("CategoryKey");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("NegativeReviews");

                    b.Property<int>("PositiveReviews");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<int>("SupplierKey");

                    b.HasKey("Id");

                    b.HasIndex("CategoryKey");

                    b.HasIndex("SupplierKey");

                    b.ToTable("DishItems");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DishItemId");

                    b.Property<int>("OrderKey");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.HasKey("Id");

                    b.HasIndex("DishItemId");

                    b.HasIndex("OrderKey");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AvailableMoneyToOrder")
                        .HasColumnType("Money");

                    b.Property<bool>("CanMultiSelect");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.WeekDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("WeekDays");
                });

            modelBuilder.Entity("Domain.DishItemToWeekDay", b =>
                {
                    b.HasOne("Domain.Entities.DishItem", "DishItem")
                        .WithMany("AvailableOn")
                        .HasForeignKey("DishItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Entities.WeekDay", "WeekDay")
                        .WithMany("AvailableItems")
                        .HasForeignKey("WeekDayId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Entities.DishItem", b =>
                {
                    b.HasOne("Domain.Entities.DishCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Domain.Entities.DishItem", "DishItem")
                        .WithMany()
                        .HasForeignKey("DishItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
