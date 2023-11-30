﻿// <auto-generated />
using System;
using EZWiki.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EZWiki.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231125223831_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("EZWiki.Models.Article", b =>
                {
                    b.Property<string>("Topic")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Published")
                        .HasColumnType("TEXT");

                    b.HasKey("Topic");

                    b.ToTable("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
