﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Concrete.Migrations
{
    [DbContext(typeof(WheelDbContext))]
    [Migration("20211017201650_ChangePictureEntity")]
    partial class ChangePictureEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Colors.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("HexCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("HexCode")
                        .IsUnique();

                    b.ToTable("Color");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Comments.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("character varying(450)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2021, 10, 17, 23, 16, 50, 114, DateTimeKind.Local).AddTicks(6632));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<int>("StarCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("WheelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WheelId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Dimensions.Dimension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Size")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("Size")
                        .IsUnique();

                    b.ToTable("Dimension");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Pictures.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("WheelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WheelId");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Producers.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Tags.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.Wheel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("CampaignPrice")
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(18,2)");

                    b.Property<int>("ProducerId")
                        .HasColumnType("integer");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<int>("StarCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("StockCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Wheel");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelCategory", b =>
                {
                    b.Property<int>("WheelId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.HasKey("WheelId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("WheelCategory");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelColor", b =>
                {
                    b.Property<int>("ColorId")
                        .HasColumnType("integer");

                    b.Property<int>("WheelId")
                        .HasColumnType("integer");

                    b.HasKey("ColorId", "WheelId");

                    b.HasIndex("WheelId");

                    b.ToTable("WheelColor");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelDimension", b =>
                {
                    b.Property<int>("DimensionId")
                        .HasColumnType("integer");

                    b.Property<int>("WheelId")
                        .HasColumnType("integer");

                    b.HasKey("DimensionId", "WheelId");

                    b.HasIndex("WheelId");

                    b.ToTable("WheelDimension");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<int>("WheelId")
                        .HasColumnType("integer");

                    b.HasKey("TagId", "WheelId");

                    b.HasIndex("WheelId");

                    b.ToTable("WheelTag");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Comments.Comment", b =>
                {
                    b.HasOne("Wheely.Core.Entities.Concrete.Wheels.Wheel", "Wheel")
                        .WithMany("Comments")
                        .HasForeignKey("WheelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wheel");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Pictures.Picture", b =>
                {
                    b.HasOne("Wheely.Core.Entities.Concrete.Wheels.Wheel", "Wheel")
                        .WithMany("Pictures")
                        .HasForeignKey("WheelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wheel");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.Wheel", b =>
                {
                    b.HasOne("Wheely.Core.Entities.Concrete.Producers.Producer", "Producer")
                        .WithMany("Wheels")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelCategory", b =>
                {
                    b.HasOne("Wheely.Core.Entities.Concrete.Categories.Category", "Category")
                        .WithMany("WheelCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wheely.Core.Entities.Concrete.Wheels.Wheel", "Wheel")
                        .WithMany("WheelCategories")
                        .HasForeignKey("WheelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Wheel");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelColor", b =>
                {
                    b.HasOne("Wheely.Core.Entities.Concrete.Colors.Color", "Color")
                        .WithMany("WheelColors")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wheely.Core.Entities.Concrete.Wheels.Wheel", "Wheel")
                        .WithMany("WheelColors")
                        .HasForeignKey("WheelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Wheel");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelDimension", b =>
                {
                    b.HasOne("Wheely.Core.Entities.Concrete.Dimensions.Dimension", "Dimension")
                        .WithMany("WheelDimensions")
                        .HasForeignKey("DimensionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wheely.Core.Entities.Concrete.Wheels.Wheel", "Wheel")
                        .WithMany("WheelDimensions")
                        .HasForeignKey("WheelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dimension");

                    b.Navigation("Wheel");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.WheelTag", b =>
                {
                    b.HasOne("Wheely.Core.Entities.Concrete.Tags.Tag", "Tag")
                        .WithMany("WheelTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wheely.Core.Entities.Concrete.Wheels.Wheel", "Wheel")
                        .WithMany("WheelTags")
                        .HasForeignKey("WheelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Wheel");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Categories.Category", b =>
                {
                    b.Navigation("WheelCategories");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Colors.Color", b =>
                {
                    b.Navigation("WheelColors");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Dimensions.Dimension", b =>
                {
                    b.Navigation("WheelDimensions");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Producers.Producer", b =>
                {
                    b.Navigation("Wheels");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Tags.Tag", b =>
                {
                    b.Navigation("WheelTags");
                });

            modelBuilder.Entity("Wheely.Core.Entities.Concrete.Wheels.Wheel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Pictures");

                    b.Navigation("WheelCategories");

                    b.Navigation("WheelColors");

                    b.Navigation("WheelDimensions");

                    b.Navigation("WheelTags");
                });
#pragma warning restore 612, 618
        }
    }
}
