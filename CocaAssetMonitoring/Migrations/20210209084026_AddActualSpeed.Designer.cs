﻿// <auto-generated />
using System;
using CocaAssetMonitoring.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CocaAssetMonitoring.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210209084026_AddActualSpeed")]
    partial class AddActualSpeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.MCAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("MachineStage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MCAttributes");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.MachineAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Power")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PowerFactor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Speed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MachineAttributes");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.MachineInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccumulativeFlag")
                        .HasColumnType("int");

                    b.Property<string>("FactoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LineNumber")
                        .HasColumnType("int");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MachineInfo");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.MachinePerformance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Availability")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EnergyConsumed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OEE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Performance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Quality")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MachinePerformance");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.MachineProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AcceptedCount")
                        .HasColumnType("int");

                    b.Property<int>("ActualSpeed")
                        .HasColumnType("int");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductType")
                        .HasColumnType("int");

                    b.Property<int>("RejectedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MachineProcesses");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PlannedRecipe")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccumulativeFlag")
                        .HasColumnType("int");

                    b.Property<decimal>("DesignedSpeed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TargetQty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.StopageReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("machineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("machineId");

                    b.ToTable("StopageReason");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.StopageReason", b =>
                {
                    b.HasOne("CocaAssetMonitoring.Models.SystemModels.MachineInfo", "machine")
                        .WithMany("StopageReasons")
                        .HasForeignKey("machineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("machine");
                });

            modelBuilder.Entity("CocaAssetMonitoring.Models.SystemModels.MachineInfo", b =>
                {
                    b.Navigation("StopageReasons");
                });
#pragma warning restore 612, 618
        }
    }
}
