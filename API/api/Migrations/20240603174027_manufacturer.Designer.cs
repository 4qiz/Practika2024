﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240603174027_manufacturer")]
    partial class manufacturer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "7a10f8a4-3c44-4f3b-8c45-0ece22771d47",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "adaacfaf-f7a4-4f58-932c-6bc9fbcce7e2",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("api.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("api.Models.IssueRequest", b =>
                {
                    b.Property<int>("IssueRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IssueRequestId"));

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IssueRequestId");

                    b.ToTable("IssueRequest", (string)null);
                });

            modelBuilder.Entity("api.Models.IssueRequestHasMedicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IssueRequestId")
                        .HasColumnType("integer");

                    b.Property<int>("MedicineId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id")
                        .HasName("PK_IssueRequestHasMedicine_1");

                    b.HasIndex("IssueRequestId");

                    b.HasIndex("MedicineId");

                    b.ToTable("IssueRequestHasMedicine", (string)null);
                });

            modelBuilder.Entity("api.Models.Manufacturer", b =>
                {
                    b.Property<int>("ManufacturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ManufacturerId"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ManufacturerId");

                    b.ToTable("Manufacturer", (string)null);

                    b.HasData(
                        new
                        {
                            ManufacturerId = 1,
                            Title = "Новартис Фарма АГ"
                        },
                        new
                        {
                            ManufacturerId = 2,
                            Title = "Татхимфармпрепараты АО"
                        });
                });

            modelBuilder.Entity("api.Models.Medicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MedicineId"));

                    b.Property<string>("Image")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(13, 2)");

                    b.Property<string>("TradeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("MedicineId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Medicine", (string)null);

                    b.HasData(
                        new
                        {
                            MedicineId = 1,
                            Image = "https://imgs.asna.ru/iblock/2d7/2d71cac199086932e4b68e6ae633eca8/100082.jpg",
                            ManufacturerId = 1,
                            Name = "Вольтарен 25мг/мл 3мл 5 шт. раствор для внутримышечного введения",
                            Price = 79m,
                            TradeName = "Вольтарен"
                        },
                        new
                        {
                            MedicineId = 2,
                            Image = "https://imgs.asna.ru/iblock/177/177882ef988b42be05abd45dbb7d5fba/816f88b93afa5c096afbeec679ffd4c0.jpg",
                            ManufacturerId = 2,
                            Name = "Кальцекс 500мг 10 шт. таблетки татхимфарм",
                            Price = 42m,
                            TradeName = "Кальцекс"
                        });
                });

            modelBuilder.Entity("api.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WarehouseId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse", (string)null);

                    b.HasData(
                        new
                        {
                            WarehouseId = 1,
                            Name = "Склад №2"
                        },
                        new
                        {
                            WarehouseId = 2,
                            Name = "Склад №2"
                        },
                        new
                        {
                            WarehouseId = 3,
                            Name = "Склад №3"
                        });
                });

            modelBuilder.Entity("api.Models.WarehouseHasMedicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .HasColumnType("integer");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("MedicineId", "WarehouseId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseHasMedicines");

                    b.HasData(
                        new
                        {
                            MedicineId = 1,
                            WarehouseId = 2,
                            Quantity = 41
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("api.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("api.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("api.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.IssueRequestHasMedicine", b =>
                {
                    b.HasOne("api.Models.IssueRequest", "IssueRequest")
                        .WithMany("IssueRequestHasMedicines")
                        .HasForeignKey("IssueRequestId")
                        .IsRequired()
                        .HasConstraintName("FK_IssueRequestHasMedicine_IssueRequest");

                    b.HasOne("api.Models.Medicine", "Medicine")
                        .WithMany("IssueRequestHasMedicines")
                        .HasForeignKey("MedicineId")
                        .IsRequired()
                        .HasConstraintName("FK_IssueRequestHasMedicine_Medicine");

                    b.Navigation("IssueRequest");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("api.Models.Medicine", b =>
                {
                    b.HasOne("api.Models.Manufacturer", "Manufacturer")
                        .WithMany("Medicines")
                        .HasForeignKey("ManufacturerId")
                        .IsRequired()
                        .HasConstraintName("FK_Medicine_Manufacturer");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("api.Models.WarehouseHasMedicine", b =>
                {
                    b.HasOne("api.Models.Medicine", "Medicine")
                        .WithMany("WarehouseHasMedicines")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseHasMedicines")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("api.Models.IssueRequest", b =>
                {
                    b.Navigation("IssueRequestHasMedicines");
                });

            modelBuilder.Entity("api.Models.Manufacturer", b =>
                {
                    b.Navigation("Medicines");
                });

            modelBuilder.Entity("api.Models.Medicine", b =>
                {
                    b.Navigation("IssueRequestHasMedicines");

                    b.Navigation("WarehouseHasMedicines");
                });

            modelBuilder.Entity("api.Models.Warehouse", b =>
                {
                    b.Navigation("WarehouseHasMedicines");
                });
#pragma warning restore 612, 618
        }
    }
}
