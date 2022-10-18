﻿// <auto-generated />
using System;
using FlightPlanner;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlightPlanner.Migrations
{
    [DbContext(typeof(FlightPlannerDbContext))]
    partial class FlightPlannerDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("FlightPlanner.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AirportName")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("FlightPlanner.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArrivalTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Carrier")
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FromId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ToId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightPlanner.Flight", b =>
                {
                    b.HasOne("FlightPlanner.Airport", "From")
                        .WithMany()
                        .HasForeignKey("FromId");

                    b.HasOne("FlightPlanner.Airport", "To")
                        .WithMany()
                        .HasForeignKey("ToId");

                    b.Navigation("From");

                    b.Navigation("To");
                });
#pragma warning restore 612, 618
        }
    }
}
