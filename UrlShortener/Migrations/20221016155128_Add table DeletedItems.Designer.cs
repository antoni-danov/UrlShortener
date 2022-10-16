﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrlShortener.Models;

#nullable disable

namespace UrlShortener.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221016155128_Add table DeletedItems")]
    partial class AddtableDeletedItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UrlShortener.Models.DeletedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedOn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedOn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UrlDataUrlId")
                        .HasColumnType("int");

                    b.Property<int>("UrlId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UrlDataUrlId");

                    b.HasIndex("UserId");

                    b.ToTable("DeletedItems");
                });

            modelBuilder.Entity("UrlShortener.Models.UrlData", b =>
                {
                    b.Property<int>("UrlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UrlId"), 1L, 1);

                    b.Property<string>("CreatedOn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UrlId");

                    b.HasIndex("UserId");

                    b.ToTable("UrlDatas");
                });

            modelBuilder.Entity("UrlShortener.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UrlShortener.Models.DeletedItem", b =>
                {
                    b.HasOne("UrlShortener.Models.UrlData", "UrlData")
                        .WithMany()
                        .HasForeignKey("UrlDataUrlId");

                    b.HasOne("UrlShortener.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UrlData");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UrlShortener.Models.UrlData", b =>
                {
                    b.HasOne("UrlShortener.Models.User", "User")
                        .WithMany("UsersUrls")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UrlShortener.Models.User", b =>
                {
                    b.Navigation("UsersUrls");
                });
#pragma warning restore 612, 618
        }
    }
}