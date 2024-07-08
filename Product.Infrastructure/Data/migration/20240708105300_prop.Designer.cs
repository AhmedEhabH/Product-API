﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product.Infrastructure.Data;

#nullable disable

namespace Product.Infrastructure.Data.migration
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240708105300_prop")]
    partial class prop
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description 1",
                            Name = "Category 1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description 2",
                            Name = "Category 2"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Description 3",
                            Name = "Category 3"
                        });
                });

            modelBuilder.Entity("Product.Core.Entities.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ProductPicture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Description 1",
                            Name = "Product 1",
                            Price = 100m,
                            ProductPicture = "http"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Description 2",
                            Name = "Product 2",
                            Price = 300m,
                            ProductPicture = "http"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "Description 3",
                            Name = "Product 3",
                            Price = 500m,
                            ProductPicture = "http"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Description = "Description 4",
                            Name = "Product 4",
                            Price = 900m,
                            ProductPicture = "http"
                        });
                });

            modelBuilder.Entity("Product.Core.Entities.Products", b =>
                {
                    b.HasOne("Product.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Product.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
