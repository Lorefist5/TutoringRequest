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
    [Migration("20240210071630_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.Property<Guid>("AccountsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("AccountRole");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastLogIn")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AvailabilitySlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<int>("Day")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
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

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MajorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MajorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Major", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("MajorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("70acc156-27c3-4890-afb3-254d2f468091"),
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("e9c30ad9-ff45-49ea-ae85-f64db8bb6bdf"),
                            RoleName = "Student"
                        },
                        new
                        {
                            Id = new Guid("e7247af7-65f5-42d6-bc45-2ac21803123e"),
                            RoleName = "Tutor"
                        });
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.TutoringSection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
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

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutoringRequest.Models.Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.AvailabilitySlot", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.Account", "Tutor")
                        .WithMany()
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

                    b.Navigation("Major");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.TutoringSection", b =>
                {
                    b.HasOne("TutoringRequest.Models.Domain.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutoringRequest.Models.Domain.Account", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutoringRequest.Models.Domain.Account", "Tutor")
                        .WithMany()
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("TutoringRequest.Models.Domain.Major", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
