﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OndeAssisto.Web.Api.Data;

namespace OndeAssisto.Web.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OndeAssisto.Common.Models.Account", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Guid");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Author", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Guid");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Genre", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Guid");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Jwt.JwtTokenRefreshData", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Media", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOutDated")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WorkGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.HasIndex("WorkGuid");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.MediaPlatform", b =>
                {
                    b.Property<Guid>("MediaGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlatformGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MediaGuid", "PlatformGuid");

                    b.HasIndex("PlatformGuid");

                    b.ToTable("MediaPlatforms");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Platform", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Site")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Guid");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Review", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MediaGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.HasIndex("MediaGuid");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Work", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GenreGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Guid");

                    b.HasIndex("AuthorGuid");

                    b.HasIndex("GenreGuid");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Media", b =>
                {
                    b.HasOne("OndeAssisto.Common.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OndeAssisto.Common.Models.Work", "Work")
                        .WithMany()
                        .HasForeignKey("WorkGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.MediaPlatform", b =>
                {
                    b.HasOne("OndeAssisto.Common.Models.Media", "Media")
                        .WithMany("Platforms")
                        .HasForeignKey("MediaGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OndeAssisto.Common.Models.Platform", "Platform")
                        .WithMany("Medias")
                        .HasForeignKey("PlatformGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Review", b =>
                {
                    b.HasOne("OndeAssisto.Common.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountGuid");

                    b.HasOne("OndeAssisto.Common.Models.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OndeAssisto.Common.Models.Work", b =>
                {
                    b.HasOne("OndeAssisto.Common.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OndeAssisto.Common.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
