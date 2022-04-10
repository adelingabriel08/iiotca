﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Signal.Server.Database;

#nullable disable

namespace Signal.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220410211024_SensorMACUniqueness")]
    partial class SensorMACUniqueness
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Signal.Server.Entities.AuthorizedSensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("MAC")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTimeUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MAC")
                        .IsUnique();

                    b.HasIndex("MAC", "ApiKey");

                    b.ToTable("AuthorizedSensors");
                });

            modelBuilder.Entity("Signal.Server.Entities.SensorStatusTrack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("SensorId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("SensorStatusTracks");
                });

            modelBuilder.Entity("Signal.Server.Entities.SensorStatusTrack", b =>
                {
                    b.HasOne("Signal.Server.Entities.AuthorizedSensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });
#pragma warning restore 612, 618
        }
    }
}
