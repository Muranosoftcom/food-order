﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(FoodOrderContext))]
    partial class FoodOrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Domain.Entities.DishItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AvailableUntil");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("NegativeReviews");

                    b.Property<int>("PositiveReviews");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.HasKey("Id");

                    b.ToTable("DishItems");
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
#pragma warning restore 612, 618
        }
    }
}
