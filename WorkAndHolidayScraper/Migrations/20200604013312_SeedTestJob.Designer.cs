﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkAndHolidayScraper.Models;

namespace WorkAndHolidayScraper.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200604013312_SeedTestJob")]
    partial class SeedTestJob
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WorkAndHolidayScraper.Models.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Href")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("OriginalWebsite")
                        .HasColumnType("text");

                    b.Property<string>("Salary")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c8bb9395-e774-4491-905d-e655e67a9d1d"),
                            Company = "Test company",
                            Date = new DateTime(2020, 6, 3, 21, 33, 12, 456, DateTimeKind.Local).AddTicks(4679),
                            Description = "Test description",
                            Href = "www.google.com",
                            Location = "Australia",
                            OriginalWebsite = "Test",
                            Salary = "a lot :)",
                            Title = "Test title"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
