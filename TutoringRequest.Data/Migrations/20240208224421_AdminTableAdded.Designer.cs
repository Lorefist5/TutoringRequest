﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TutoringRequest.Data;

#nullable disable

namespace TutoringRequest.Data.Migrations
{
    [DbContext(typeof(TutoringDbContext))]
    [Migration("20240208224421_AdminTableAdded")]
    partial class AdminTableAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("TutoringRequest.Models.Domain.AdminAccountInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastLogIn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AdminId")
                        .IsUnique();

                    b.ToTable("AdminAccountInfos");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AdminRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AdminAccountInfoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AdminAccountInfoId");

                    b.ToTable("AdminRoles");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Administrator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("InfoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AvailabilitySlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Day")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TutorId");

                    b.ToTable("AvailabilitySlots");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MajorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TutorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MajorId");

                    b.HasIndex("TutorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Major", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("MajorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StudentNumber")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Tutor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TutorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.TutoringSection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TutorId");

                    b.ToTable("TutoringSections");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AdminAccountInfo", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.Administrator", "Administrator")
                        .WithOne("AdminAccountInfo")
                        .HasForeignKey("TutoringRequest.Models.Domain.AdminAccountInfo", "AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AdminRole", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.AdminAccountInfo", null)
                        .WithMany("AdminRoles")
                        .HasForeignKey("AdminAccountInfoId");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AvailabilitySlot", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.Tutor", "Tutor")
                        .WithMany("AvailabilitySlots")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Course", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.Major", "Major")
                        .WithMany("Courses")
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutoringRequest.Models.Domain.Tutor", null)
                        .WithMany("Courses")
                        .HasForeignKey("TutorId");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.TutoringSection", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutoringRequest.Models.Domain.Student", "Student")
                        .WithMany("TutoringSections")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutoringRequest.Models.Domain.Tutor", "Tutor")
                        .WithMany("TutoringSections")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AdminAccountInfo", b =>
                {
                    b.Navigation("AdminRoles");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Administrator", b =>
                {
                    b.Navigation("AdminAccountInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Major", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Student", b =>
                {
                    b.Navigation("TutoringSections");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Tutor", b =>
                {
                    b.Navigation("AvailabilitySlots");

                    b.Navigation("Courses");

                    b.Navigation("TutoringSections");
                });
#pragma warning restore 612, 618
        }
    }
}