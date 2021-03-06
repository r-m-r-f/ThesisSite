﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisSite.Data;

namespace ThesisSite.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190109002836_UpdateFileUpload")]
    partial class UpdateFileUpload
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ThesisSite.Data.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("ThesisSite.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("IndexNumber");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ThesisSite.Domain.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset>("DueTo");

                    b.Property<int>("GroupId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("ShortDescription");

                    b.Property<int>("UploadLimit");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("ThesisSite.Domain.AssignmetsToStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignmentId");

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignmetsToStudent");
                });

            modelBuilder.Entity("ThesisSite.Domain.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("Language");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ShortDescription");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ThesisSite.Domain.FileUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignmentId");

                    b.Property<int>("AssignmentToStudentId");

                    b.Property<int?>("AssignmetsToStudentId");

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Path");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<int>("TopicId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("AssignmetsToStudentId");

                    b.HasIndex("UserId");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("ThesisSite.Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignmentId");

                    b.Property<int>("CourseID");

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("Limit");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CourseID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ThesisSite.Domain.GroupEnrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<int>("GroupId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupEnrollments");
                });

            modelBuilder.Entity("ThesisSite.Domain.LanguageVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<int>("Language");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("LanguageVersion");
                });

            modelBuilder.Entity("ThesisSite.Domain.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignmentId");

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("Limit");

                    b.Property<string>("Name");

                    b.Property<string>("ShortDescription");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("ThesisSite.Domain.TopicToStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignmentId");

                    b.Property<DateTimeOffset>("CreatedTimestamp");

                    b.Property<DateTimeOffset?>("DeletedTimestamp");

                    b.Property<int>("FileUploadId");

                    b.Property<int>("Grade");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("TopicId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("FileUploadId");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("TopicToStudents");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ThesisSite.Data.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ThesisSite.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ThesisSite.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("ThesisSite.Data.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisSite.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ThesisSite.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThesisSite.Domain.Assignment", b =>
                {
                    b.HasOne("ThesisSite.Domain.Group", "Group")
                        .WithMany("Assignments")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThesisSite.Domain.AssignmetsToStudent", b =>
                {
                    b.HasOne("ThesisSite.Domain.Assignment", "Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisSite.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ThesisSite.Domain.FileUpload", b =>
                {
                    b.HasOne("ThesisSite.Domain.Assignment", "Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisSite.Domain.AssignmetsToStudent", "AssignmetsToStudent")
                        .WithMany("Uploads")
                        .HasForeignKey("AssignmetsToStudentId");

                    b.HasOne("ThesisSite.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ThesisSite.Domain.Group", b =>
                {
                    b.HasOne("ThesisSite.Domain.Course", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThesisSite.Domain.GroupEnrollment", b =>
                {
                    b.HasOne("ThesisSite.Domain.Group", "Group")
                        .WithMany("GroupEnrollments")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisSite.Domain.ApplicationUser", "User")
                        .WithMany("GroupEnrollments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ThesisSite.Domain.LanguageVersion", b =>
                {
                    b.HasOne("ThesisSite.Domain.Course", "Course")
                        .WithMany("LanguageVersions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThesisSite.Domain.Topic", b =>
                {
                    b.HasOne("ThesisSite.Domain.Assignment", "Assignment")
                        .WithMany("Topics")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThesisSite.Domain.TopicToStudent", b =>
                {
                    b.HasOne("ThesisSite.Domain.Assignment")
                        .WithMany("TopicToStudents")
                        .HasForeignKey("AssignmentId");

                    b.HasOne("ThesisSite.Domain.FileUpload", "FileUpload")
                        .WithMany()
                        .HasForeignKey("FileUploadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisSite.Domain.Topic", "Topic")
                        .WithMany("Students")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisSite.Domain.ApplicationUser", "User")
                        .WithMany("Topics")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
