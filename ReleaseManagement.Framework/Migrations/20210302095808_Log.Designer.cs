﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReleaseManagement.Framework.Data;

namespace ReleaseManagement.Framework.Migrations
{
    [DbContext(typeof(ReleaseDataContext))]
    [Migration("20210302095808_Log")]
    partial class Log
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ComponentTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComponentTypeId");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.ComponentApproval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ApprovalDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Approved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApprovedById")
                        .HasColumnType("TEXT");

                    b.Property<int>("ComponentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReleaseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("ReleaseId");

                    b.ToTable("ComponentApproval");
                });

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.ComponentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ComponentType");
                });

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Exception")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.Release", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReleaseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Release");
                });

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.Component", b =>
                {
                    b.HasOne("ReleaseManagement.Framework.Data.Model.ComponentType", "ComponentType")
                        .WithMany("Components")
                        .HasForeignKey("ComponentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComponentType");
                });

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.ComponentApproval", b =>
                {
                    b.HasOne("ReleaseManagement.Framework.Data.Model.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReleaseManagement.Framework.Data.Model.Release", "Release")
                        .WithMany()
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");

                    b.Navigation("Release");
                });

            modelBuilder.Entity("ReleaseManagement.Framework.Data.Model.ComponentType", b =>
                {
                    b.Navigation("Components");
                });
#pragma warning restore 612, 618
        }
    }
}
