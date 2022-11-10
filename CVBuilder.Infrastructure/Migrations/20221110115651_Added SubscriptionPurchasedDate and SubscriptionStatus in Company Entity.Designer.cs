﻿// <auto-generated />
using System;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CVBuilder.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221110115651_Added SubscriptionPurchasedDate and SubscriptionStatus in Company Entity")]
    partial class AddedSubscriptionPurchasedDateandSubscriptionStatusinCompanyEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CVBuilder.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubscriptionPurchasedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriptionStatus")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.CompanyDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("CompanyDetails");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.CVRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestsAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("cVRequests");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Degree", b =>
                {
                    b.Property<int>("DegreeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DegreeId"), 1L, 1);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("DegreeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.DegreeUpdateRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DegreeId")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("DegreeUpdateRequests");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.PersonalDetailsUpdateRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("PersonalDetailsUpdateRequests");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"), 1L, 1);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ProjectId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.ProjectUpdateRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("ProjectUpdateRequests");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.ResourceRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"), 1L, 1);

                    b.Property<Guid>("AppliedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReviewedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.ToTable("ResourceRequests");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillId"), 1L, 1);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("SkillId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.SkillUpdateRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int?>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("SkillUpdateRequests");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.WorkExperience", b =>
                {
                    b.Property<int>("WorkExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkExperienceId"), 1L, 1);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("WorkExperienceId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("WorkExperiences");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.WorkExperienceUpdateRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkExperienceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("WorkExperienceUpdateRequests");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.CompanyDetails", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.Company", null)
                        .WithOne("CompanyDetails")
                        .HasForeignKey("CVBuilder.Domain.Entities.CompanyDetails", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.CVRequest", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.Company", null)
                        .WithMany("CVRequests")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Degree", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.Employee", null)
                        .WithMany("Degrees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.DegreeDetails", "DegreeDetails", b1 =>
                        {
                            b1.Property<int>("DegreeId")
                                .HasColumnType("int");

                            b1.Property<string>("Institute")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Institute");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.Property<string>("Subject")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("")
                                .HasColumnName("Subject");

                            b1.HasKey("DegreeId");

                            b1.ToTable("Degrees");

                            b1.WithOwner()
                                .HasForeignKey("DegreeId");
                        });

                    b.Navigation("DegreeDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.DegreeUpdateRequest", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.ResourceRequest", null)
                        .WithOne("DegreeUpdateRequest")
                        .HasForeignKey("CVBuilder.Domain.Entities.DegreeUpdateRequest", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.DegreeDetails", "DegreeDetails", b1 =>
                        {
                            b1.Property<int>("DegreeUpdateRequestId")
                                .HasColumnType("int");

                            b1.Property<string>("Institute")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Institute");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.Property<string>("Subject")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Subject");

                            b1.HasKey("DegreeUpdateRequestId");

                            b1.ToTable("DegreeUpdateRequests");

                            b1.WithOwner()
                                .HasForeignKey("DegreeUpdateRequestId");
                        });

                    b.Navigation("DegreeDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.PersonalDetailsUpdateRequest", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.ResourceRequest", null)
                        .WithOne("PersonalDetailsUpdateRequest")
                        .HasForeignKey("CVBuilder.Domain.Entities.PersonalDetailsUpdateRequest", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Project", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.Employee", null)
                        .WithMany("Projects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.ProjectDetails", "ProjectDetails", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.Property<string>("Link")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Link");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projects");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.Navigation("ProjectDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.ProjectUpdateRequest", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.ResourceRequest", null)
                        .WithOne("ProjectUpdateRequest")
                        .HasForeignKey("CVBuilder.Domain.Entities.ProjectUpdateRequest", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.ProjectDetails", "ProjectDetails", b1 =>
                        {
                            b1.Property<int>("ProjectUpdateRequestId")
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.Property<string>("Link")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Link");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("ProjectUpdateRequestId");

                            b1.ToTable("ProjectUpdateRequests");

                            b1.WithOwner()
                                .HasForeignKey("ProjectUpdateRequestId");
                        });

                    b.Navigation("ProjectDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Skill", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.Employee", null)
                        .WithMany("Skills")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.SkillDetails", "SkillDetails", b1 =>
                        {
                            b1.Property<int>("SkillId")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("SkillId");

                            b1.ToTable("Skills");

                            b1.WithOwner()
                                .HasForeignKey("SkillId");
                        });

                    b.Navigation("SkillDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.SkillUpdateRequest", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.ResourceRequest", null)
                        .WithOne("SkillUpdateRequest")
                        .HasForeignKey("CVBuilder.Domain.Entities.SkillUpdateRequest", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.SkillDetails", "SkillDetails", b1 =>
                        {
                            b1.Property<int>("SkillUpdateRequestId")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("SkillUpdateRequestId");

                            b1.ToTable("SkillUpdateRequests");

                            b1.WithOwner()
                                .HasForeignKey("SkillUpdateRequestId");
                        });

                    b.Navigation("SkillDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.WorkExperience", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.Employee", null)
                        .WithMany("WorkExperiences")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.WorkExperienceDetails", "WorkExperienceDetails", b1 =>
                        {
                            b1.Property<int>("WorkExperienceId")
                                .HasColumnType("int");

                            b1.Property<string>("Company")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Company");

                            b1.Property<string>("Designation")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Designation");

                            b1.Property<DateTime?>("EndDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime?>("StartDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("StartDate");

                            b1.HasKey("WorkExperienceId");

                            b1.ToTable("WorkExperiences");

                            b1.WithOwner()
                                .HasForeignKey("WorkExperienceId");
                        });

                    b.Navigation("WorkExperienceDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.WorkExperienceUpdateRequest", b =>
                {
                    b.HasOne("CVBuilder.Domain.Entities.ResourceRequest", null)
                        .WithOne("WorkExperienceUpdateRequest")
                        .HasForeignKey("CVBuilder.Domain.Entities.WorkExperienceUpdateRequest", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CVBuilder.Domain.ValueObjects.WorkExperienceDetails", "WorkExperienceDetails", b1 =>
                        {
                            b1.Property<int>("WorkExperienceUpdateRequestId")
                                .HasColumnType("int");

                            b1.Property<string>("Company")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Company");

                            b1.Property<string>("Designation")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Designation");

                            b1.Property<DateTime?>("EndDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime?>("StartDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("StartDate");

                            b1.HasKey("WorkExperienceUpdateRequestId");

                            b1.ToTable("WorkExperienceUpdateRequests");

                            b1.WithOwner()
                                .HasForeignKey("WorkExperienceUpdateRequestId");
                        });

                    b.Navigation("WorkExperienceDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Company", b =>
                {
                    b.Navigation("CVRequests");

                    b.Navigation("CompanyDetails");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.Employee", b =>
                {
                    b.Navigation("Degrees");

                    b.Navigation("Projects");

                    b.Navigation("Skills");

                    b.Navigation("WorkExperiences");
                });

            modelBuilder.Entity("CVBuilder.Domain.Entities.ResourceRequest", b =>
                {
                    b.Navigation("DegreeUpdateRequest");

                    b.Navigation("PersonalDetailsUpdateRequest");

                    b.Navigation("ProjectUpdateRequest");

                    b.Navigation("SkillUpdateRequest");

                    b.Navigation("WorkExperienceUpdateRequest");
                });
#pragma warning restore 612, 618
        }
    }
}
