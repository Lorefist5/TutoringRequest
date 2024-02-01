﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TutoringRequest.Data;

#nullable disable

namespace TutoringRequest.Data.Migrations
{
    [DbContext(typeof(TutoringDbContext))]
    partial class TutoringDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("TutoringRequest.Models.Domain.AvailabilitySlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
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
