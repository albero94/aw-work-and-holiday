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
    [Migration("20200605125503_FollowPostgresStandards")]
    partial class FollowPostgresStandards
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
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .HasColumnName("company")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Href")
                        .IsRequired()
                        .HasColumnName("href")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnName("location")
                        .HasColumnType("text");

                    b.Property<string>("OriginalWebsite")
                        .HasColumnName("original_website")
                        .HasColumnType("text");

                    b.Property<string>("Salary")
                        .HasColumnName("salary")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("job");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9169a369-dc68-45ea-a6cd-c56318d1bd44"),
                            Company = "Test company",
                            Date = new DateTime(2020, 6, 5, 8, 55, 2, 359, DateTimeKind.Local).AddTicks(505),
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
