﻿// <auto-generated />
using System;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(BigAccountingContext))]
    [Migration("20200713202825_AddDeptorsCountToDeptTBL")]
    partial class AddDeptorsCountToDeptTBL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Models.Creditor", b =>
                {
                    b.Property<int>("CreditorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("GetMoney");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CreditorID");

                    b.ToTable("Creditors");
                });

            modelBuilder.Entity("DataLayer.Models.Dept", b =>
                {
                    b.Property<int>("DeptID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<bool>("Delete");

                    b.Property<int>("DeptorsCount");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsPaid");

                    b.Property<decimal>("Price");

                    b.HasKey("DeptID");

                    b.ToTable("Depts");
                });

            modelBuilder.Entity("DataLayer.Models.Dept_Creditor", b =>
                {
                    b.Property<int>("DeptID");

                    b.Property<int>("CreditorID");

                    b.HasKey("DeptID", "CreditorID");

                    b.HasIndex("CreditorID");

                    b.ToTable("Dept_Creditors");
                });

            modelBuilder.Entity("DataLayer.Models.Dept_Deptor", b =>
                {
                    b.Property<int>("DeptID");

                    b.Property<int>("DeptorID");

                    b.HasKey("DeptID", "DeptorID");

                    b.HasIndex("DeptorID");

                    b.ToTable("Dept_Deptors");
                });

            modelBuilder.Entity("DataLayer.Models.Deptor", b =>
                {
                    b.Property<int>("DeptorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("DeptMoney");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("DeptorID");

                    b.ToTable("Deptors");
                });

            modelBuilder.Entity("DataLayer.Models.Member", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreditorID");

                    b.Property<int>("DeptorID");

                    b.Property<string>("Mobile")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("MemberID");

                    b.HasIndex("CreditorID")
                        .IsUnique();

                    b.HasIndex("DeptorID")
                        .IsUnique();

                    b.ToTable("Members");
                });

            modelBuilder.Entity("DataLayer.Models.Dept_Creditor", b =>
                {
                    b.HasOne("DataLayer.Models.Creditor", "Creditor")
                        .WithMany("Dept_Creditors")
                        .HasForeignKey("CreditorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Models.Dept", "Dept")
                        .WithMany("Dept_Creditors")
                        .HasForeignKey("DeptID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Models.Dept_Deptor", b =>
                {
                    b.HasOne("DataLayer.Models.Dept", "Dept")
                        .WithMany("Dept_Deptors")
                        .HasForeignKey("DeptID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Models.Deptor", "Deptor")
                        .WithMany("Dept_Deptors")
                        .HasForeignKey("DeptorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Models.Member", b =>
                {
                    b.HasOne("DataLayer.Models.Creditor", "Creditor")
                        .WithOne("Member")
                        .HasForeignKey("DataLayer.Models.Member", "CreditorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Models.Deptor", "Deptor")
                        .WithOne("Member")
                        .HasForeignKey("DataLayer.Models.Member", "DeptorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
