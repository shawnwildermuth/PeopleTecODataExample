﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persontec.Api.Data;

namespace Persontec.Api.Migrations
{
    [DbContext(typeof(HrContext))]
    [Migration("20200213153155_TransfersOrgs")]
    partial class TransfersOrgs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Persontec.Api.Data.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeNumber")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            EmployeeNumber = 101,
                            FirstName = "Dennis",
                            LastName = "Dunaway",
                            OrganizationId = 1
                        },
                        new
                        {
                            EmployeeId = 2,
                            EmployeeNumber = 115,
                            FirstName = "James",
                            LastName = "Smith",
                            OrganizationId = 1,
                            SupervisorId = 1
                        },
                        new
                        {
                            EmployeeId = 3,
                            EmployeeNumber = 1016,
                            FirstName = "Jake ",
                            LastName = "Dwight",
                            OrganizationId = 1,
                            SupervisorId = 2
                        },
                        new
                        {
                            EmployeeId = 4,
                            EmployeeNumber = 1010,
                            FirstName = "Tim",
                            LastName = "Tunney",
                            OrganizationId = 1,
                            SupervisorId = 2
                        },
                        new
                        {
                            EmployeeId = 5,
                            EmployeeNumber = 102,
                            FirstName = "Alec",
                            LastName = "Smart",
                            OrganizationId = 1,
                            SupervisorId = 4
                        });
                });

            modelBuilder.Entity("Persontec.Api.Data.Entities.EmploymentPeriod", b =>
                {
                    b.Property<int>("EmploymentPeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("EmploymentPeriodId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmploymentPeriod");

                    b.HasData(
                        new
                        {
                            EmploymentPeriodId = 1,
                            EmployeeId = 1,
                            EndingDate = new DateTime(2020, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingDate = new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Active"
                        });
                });

            modelBuilder.Entity("Persontec.Api.Data.Entities.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationCode")
                        .HasColumnType("int");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("VicePresidentEmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("VicePresidentId")
                        .HasColumnType("int");

                    b.HasKey("OrganizationId");

                    b.HasIndex("VicePresidentEmployeeId");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            OrganizationId = 1,
                            OrganizationCode = 12345,
                            OrganizationName = "Group J",
                            VicePresidentId = 1
                        });
                });

            modelBuilder.Entity("Persontec.Api.Data.Entities.Transfer", b =>
                {
                    b.Property<int>("TransferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrentOrganizationOrganizationId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TransferId");

                    b.HasIndex("CurrentOrganizationOrganizationId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Transfers");

                    b.HasData(
                        new
                        {
                            TransferId = 1,
                            EmployeeId = 1,
                            EndingDate = new DateTime(2020, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingDate = new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TransferId = 2,
                            EmployeeId = 1,
                            EndingDate = new DateTime(2014, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingDate = new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Persontec.Api.Data.Entities.Employee", b =>
                {
                    b.HasOne("Persontec.Api.Data.Entities.Employee", null)
                        .WithMany("DirectReports")
                        .HasForeignKey("SupervisorId");
                });

            modelBuilder.Entity("Persontec.Api.Data.Entities.EmploymentPeriod", b =>
                {
                    b.HasOne("Persontec.Api.Data.Entities.Employee", null)
                        .WithMany("EmploymentPeriods")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Persontec.Api.Data.Entities.Organization", b =>
                {
                    b.HasOne("Persontec.Api.Data.Entities.Employee", null)
                        .WithOne("Organization")
                        .HasForeignKey("Persontec.Api.Data.Entities.Organization", "OrganizationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Persontec.Api.Data.Entities.Employee", "VicePresident")
                        .WithMany()
                        .HasForeignKey("VicePresidentEmployeeId");
                });

            modelBuilder.Entity("Persontec.Api.Data.Entities.Transfer", b =>
                {
                    b.HasOne("Persontec.Api.Data.Entities.Organization", "CurrentOrganization")
                        .WithMany()
                        .HasForeignKey("CurrentOrganizationOrganizationId");

                    b.HasOne("Persontec.Api.Data.Entities.Employee", "Employee")
                        .WithMany("Transfers")
                        .HasForeignKey("EmployeeId");
                });
#pragma warning restore 612, 618
        }
    }
}
