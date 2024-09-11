﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SSTAlumniAssociation.Core.Context;

#nullable disable

namespace SSTAlumniAssociation.WebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240911084649_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GroupMember", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.HasKey("GroupsId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("GroupMember");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CtaTitle")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("CtaUrl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("HeroImageAlt")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("HeroImageUrl")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AdmittedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("AdmittedById")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AdmittedById");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("105d9b12-a084-4417-bf68-742e33bab9d9"),
                            EventId = new Guid("bc7016f8-2f9d-4057-a3ae-58828d66522c"),
                            UserId = new Guid("df90f5ea-a236-413f-a6c1-ca9197427631")
                        },
                        new
                        {
                            Id = new Guid("66cd9cb6-2ed1-480d-bb1d-f4d1680177f5"),
                            EventId = new Guid("1710e6a9-f8e5-4456-b5a4-3c7d8055159f"),
                            UserId = new Guid("df90f5ea-a236-413f-a6c1-ca9197427631")
                        });
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.CheckIn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CheckInDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CheckOutDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ServiceAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServiceAccountId");

                    b.ToTable("CheckIns");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("BadgeImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bc7016f8-2f9d-4057-a3ae-58828d66522c"),
                            Active = true,
                            BadgeImage = "https://picsum.photos/200",
                            Description = "A very warm homecoming.",
                            EndDateTime = new DateTime(2024, 9, 21, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9920),
                            Location = "SST Multi-Purpose Hall",
                            Name = "Homecoming 2024",
                            StartDateTime = new DateTime(2024, 9, 11, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9920)
                        },
                        new
                        {
                            Id = new Guid("1710e6a9-f8e5-4456-b5a4-3c7d8055159f"),
                            Active = false,
                            BadgeImage = "https://picsum.photos/200",
                            Description = "A very warm homecoming.",
                            EndDateTime = new DateTime(2024, 9, 21, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9940),
                            Location = "SST Multi-Purpose Hall",
                            Name = "Trial",
                            StartDateTime = new DateTime(2024, 9, 11, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9940)
                        });
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.MembershipPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("MembershipPlans");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.MembershipSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MembershipPlanId")
                        .HasColumnType("uuid");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("text");

                    b.Property<string>("PaymentIntentState")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("MembershipPlanId");

                    b.ToTable("MembershipSubscriptions");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirebaseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FirebaseId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.GuestCheckIn", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.CheckIn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nric")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("GuestCheckIns");

                    b.HasData(
                        new
                        {
                            Id = new Guid("696b775b-effa-46fd-880e-5a9c34d0966c"),
                            CheckInDateTime = new DateTime(2024, 9, 11, 8, 46, 48, 691, DateTimeKind.Utc).AddTicks(80),
                            ServiceAccountId = new Guid("a78a112f-3355-499e-aafd-824c14858b34"),
                            Name = "Alex",
                            Nric = "999B",
                            Phone = "9999 9999",
                            Reason = "Alex is bored"
                        });
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.UserCheckIn", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.CheckIn");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("UserId");

                    b.ToTable("UserCheckIns");

                    b.HasData(
                        new
                        {
                            Id = new Guid("732faab7-22e4-406b-9339-915d223ac2f2"),
                            CheckInDateTime = new DateTime(2024, 9, 11, 8, 46, 48, 691, DateTimeKind.Utc).AddTicks(40),
                            ServiceAccountId = new Guid("a78a112f-3355-499e-aafd-824c14858b34"),
                            UserId = new Guid("df90f5ea-a236-413f-a6c1-ca9197427631")
                        },
                        new
                        {
                            Id = new Guid("3dc67aff-20d5-4299-a251-f413f0a8f4d0"),
                            CheckInDateTime = new DateTime(2024, 9, 11, 8, 46, 48, 691, DateTimeKind.Utc).AddTicks(60),
                            ServiceAccountId = new Guid("a78a112f-3355-499e-aafd-824c14858b34"),
                            UserId = new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f")
                        });
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Employee", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.User");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Member", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.User");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Membership")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("MemberId")
                        .IsUnique();

                    b.ToTable("Members");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.ServiceAccount", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.User");

                    b.Property<string>("ServiceAccountType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("ServiceAccounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a78a112f-3355-499e-aafd-824c14858b34"),
                            Email = "guardhouse+laptop@sstaa.org",
                            FirebaseId = "EGjzDDZgHxXd80aGUYkeoP5fNnC2",
                            Name = "Guard House Laptop",
                            ServiceAccountType = "GuardHouse"
                        });
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.SystemAdmin", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.User");

                    b.ToTable("SystemAdmins");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.AlumniMember", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.Member");

                    b.Property<int?>("GraduationYear")
                        .HasColumnType("integer");

                    b.ToTable("AlumniMembers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"),
                            Email = "qinguan20040914@gmail.com",
                            FirebaseId = "GuZZVeOdlhNsf5dZGQmU2yV1Ox33",
                            Name = "Qin Guan",
                            MemberId = "EXCO-1",
                            Membership = "Exco",
                            GraduationYear = 2000
                        },
                        new
                        {
                            Id = new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"),
                            Email = "tan_zheng_jie@sstaa.org",
                            FirebaseId = "5ZPERFPTvfMfxwhH7SGsOmXqSco2",
                            Name = "Tan Zheng Jie",
                            MemberId = "EXCO-2",
                            Membership = "Exco"
                        });
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.EmployeeMember", b =>
                {
                    b.HasBaseType("SSTAlumniAssociation.Core.Entities.Member");

                    b.Property<int?>("GraduationYear")
                        .HasColumnType("integer");

                    b.ToTable("EmployeeMembers");
                });

            modelBuilder.Entity("GroupMember", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSTAlumniAssociation.Core.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Attendee", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.ServiceAccount", "AdmittedBy")
                        .WithMany("AttendeesAdmitted")
                        .HasForeignKey("AdmittedById");

                    b.HasOne("SSTAlumniAssociation.Core.Entities.Event", "Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSTAlumniAssociation.Core.Entities.User", "User")
                        .WithMany("UserEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdmittedBy");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.CheckIn", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.ServiceAccount", "ServiceAccount")
                        .WithMany()
                        .HasForeignKey("ServiceAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceAccount");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Event", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.User", null)
                        .WithMany("Events")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.MembershipSubscription", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.Member", "Member")
                        .WithMany("Subscriptions")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSTAlumniAssociation.Core.Entities.MembershipPlan", "MembershipPlan")
                        .WithMany()
                        .HasForeignKey("MembershipPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("MembershipPlan");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.GuestCheckIn", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.CheckIn", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.GuestCheckIn", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.UserCheckIn", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.CheckIn", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.UserCheckIn", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSTAlumniAssociation.Core.Entities.User", "User")
                        .WithMany("CheckIns")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Employee", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Member", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.Member", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.ServiceAccount", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.ServiceAccount", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.SystemAdmin", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.SystemAdmin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.AlumniMember", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.Member", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.AlumniMember", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.EmployeeMember", b =>
                {
                    b.HasOne("SSTAlumniAssociation.Core.Entities.Member", null)
                        .WithOne()
                        .HasForeignKey("SSTAlumniAssociation.Core.Entities.EmployeeMember", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Event", b =>
                {
                    b.Navigation("Attendees");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.User", b =>
                {
                    b.Navigation("CheckIns");

                    b.Navigation("Events");

                    b.Navigation("UserEvents");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.Member", b =>
                {
                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("SSTAlumniAssociation.Core.Entities.ServiceAccount", b =>
                {
                    b.Navigation("AttendeesAdmitted");
                });
#pragma warning restore 612, 618
        }
    }
}
