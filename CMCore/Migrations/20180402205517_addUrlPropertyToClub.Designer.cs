﻿// <auto-generated />
using CMCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CMCore.Migrations
{
    [DbContext(typeof(ContentManagerDbContext))]
    [Migration("20180402205517_addUrlPropertyToClub")]
    partial class addUrlPropertyToClub
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("CMCore.Models.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("CMCore.Models.Companie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CMCore.Models.Countrie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CMCore.Models.Extension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Extensions");
                });

            modelBuilder.Entity("CMCore.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("ExtensionId");

                    b.Property<string>("Name");

                    b.Property<string>("PathName");

                    b.HasKey("Id");

                    b.HasIndex("ExtensionId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("CMCore.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.ClubRegion", b =>
                {
                    b.Property<int>("ClubId");

                    b.Property<int>("RegionId");

                    b.HasKey("ClubId", "RegionId");

                    b.HasIndex("RegionId");

                    b.ToTable("ClubRegions");
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.ClubType", b =>
                {
                    b.Property<int>("ClubId");

                    b.Property<int>("TypeId");

                    b.HasKey("ClubId", "TypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("ClubTypes");
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.FileClub", b =>
                {
                    b.Property<int>("FileId");

                    b.Property<int>("ClubId");

                    b.HasKey("FileId", "ClubId");

                    b.HasIndex("ClubId");

                    b.ToTable("FileClubs");
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.FileCompanie", b =>
                {
                    b.Property<int>("FileId");

                    b.Property<int>("CompanieId");

                    b.HasKey("FileId", "CompanieId");

                    b.HasIndex("CompanieId");

                    b.ToTable("FileCompanies");
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.FileTag", b =>
                {
                    b.Property<int>("FileId");

                    b.Property<int>("TagId");

                    b.HasKey("FileId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("FileTags");
                });

            modelBuilder.Entity("CMCore.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CMCore.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("CMCore.Models.Countrie", b =>
                {
                    b.HasOne("CMCore.Models.Region", "Region")
                        .WithMany("Countries")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMCore.Models.File", b =>
                {
                    b.HasOne("CMCore.Models.Extension", "Extension")
                        .WithMany("Files")
                        .HasForeignKey("ExtensionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.ClubRegion", b =>
                {
                    b.HasOne("CMCore.Models.Club", "Club")
                        .WithMany("ClubRegions")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMCore.Models.Region", "Region")
                        .WithMany("ClubRegions")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.ClubType", b =>
                {
                    b.HasOne("CMCore.Models.Club", "Club")
                        .WithMany("ClubTypes")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMCore.Models.Type", "Type")
                        .WithMany("ClubTypes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.FileClub", b =>
                {
                    b.HasOne("CMCore.Models.Club", "Club")
                        .WithMany("FileClubs")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMCore.Models.File", "File")
                        .WithMany("FileClubs")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.FileCompanie", b =>
                {
                    b.HasOne("CMCore.Models.Companie", "Companie")
                        .WithMany("FileCompanies")
                        .HasForeignKey("CompanieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMCore.Models.File", "File")
                        .WithMany("FileCompanies")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMCore.Models.RelacionClass.FileTag", b =>
                {
                    b.HasOne("CMCore.Models.File", "File")
                        .WithMany("FileTags")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMCore.Models.Tag", "Tag")
                        .WithMany("FileTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
