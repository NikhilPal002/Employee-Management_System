﻿// <auto-generated />
using System;
using Employee_management.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Employee_Management_System.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    [Migration("20250207052044_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Employee_management.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceID"));

                    b.Property<TimeSpan?>("CheckInTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("CheckOutTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.HasKey("AttendanceID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("Employee_management.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Employee_management.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("RoleID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Employee_management.Models.Leave", b =>
                {
                    b.Property<int>("LeaveID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaveID"));

                    b.Property<DateTime>("AppliedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ApprovedBy")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeaveType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LeaveID");

                    b.HasIndex("ApprovedBy");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("Employee_management.Models.Payroll", b =>
                {
                    b.Property<int>("PayrollID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayrollID"));

                    b.Property<decimal>("BasicSalary")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Deductions")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("MonthYear")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("NetSalary")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PayrollID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Payrolls");
                });

            modelBuilder.Entity("Employee_management.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleID = 2,
                            RoleName = "HR"
                        },
                        new
                        {
                            RoleID = 3,
                            RoleName = "Employee"
                        });
                });

            modelBuilder.Entity("Employee_management.Models.Attendance", b =>
                {
                    b.HasOne("Employee_management.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Employee_management.Models.Employee", b =>
                {
                    b.HasOne("Employee_management.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID");

                    b.HasOne("Employee_management.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Employee_management.Models.Leave", b =>
                {
                    b.HasOne("Employee_management.Models.Employee", "ApprovedByEmployee")
                        .WithMany()
                        .HasForeignKey("ApprovedBy");

                    b.HasOne("Employee_management.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedByEmployee");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Employee_management.Models.Payroll", b =>
                {
                    b.HasOne("Employee_management.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
