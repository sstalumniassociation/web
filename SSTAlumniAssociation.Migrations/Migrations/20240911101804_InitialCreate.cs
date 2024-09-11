using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SSTAlumniAssociation.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    HeroImageAlt = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    HeroImageUrl = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    CtaTitle = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    CtaUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BuiltIn = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirebaseId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    BadgeImage = table.Column<string>(type: "text", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceAccountType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAccounts_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemAdmins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemAdmins_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlumniMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GraduationYear = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumniMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlumniMembers_Members_Id",
                        column: x => x.Id,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GraduationYear = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMembers_Members_Id",
                        column: x => x.Id,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => new { x.GroupsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_GroupMember_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "text", nullable: true),
                    PaymentIntentState = table.Column<string>(type: "text", nullable: true),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipPlanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipSubscriptions_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembershipSubscriptions_MembershipPlans_MembershipPlanId",
                        column: x => x.MembershipPlanId,
                        principalTable: "MembershipPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdmittedById = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendees_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendees_ServiceAccounts_AdmittedById",
                        column: x => x.AdmittedById,
                        principalTable: "ServiceAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attendees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckIns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckOutDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ServiceAccountId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckIns_ServiceAccounts_ServiceAccountId",
                        column: x => x.ServiceAccountId,
                        principalTable: "ServiceAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipSubscriptionPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Processor = table.Column<int>(type: "integer", nullable: false),
                    IntentId = table.Column<string>(type: "text", nullable: false),
                    IntentState = table.Column<string>(type: "text", nullable: false),
                    MembershipSubscriptionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipSubscriptionPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipSubscriptionPayment_MembershipSubscriptions_Membe~",
                        column: x => x.MembershipSubscriptionId,
                        principalTable: "MembershipSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuestCheckIns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Nric = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestCheckIns_CheckIns_Id",
                        column: x => x.Id,
                        principalTable: "CheckIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCheckIns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCheckIns_CheckIns_Id",
                        column: x => x.Id,
                        principalTable: "CheckIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCheckIns_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Active", "BadgeImage", "Description", "EndDateTime", "Location", "Name", "StartDateTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("535bb727-f3eb-4cb3-adcf-aef04f14e82a"), true, "https://picsum.photos/200", "A very warm homecoming.", new DateTime(2024, 1, 13, 15, 0, 0, 0, DateTimeKind.Utc), "SST Multi-Purpose Hall", "Homecoming 2024", new DateTime(2024, 1, 13, 9, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f2c84690-0311-4563-9635-ad0982cc1229"), false, "https://picsum.photos/200", "Class of 2014 Reunion", new DateTime(2024, 8, 17, 16, 0, 0, 0, DateTimeKind.Utc), "HIGHfive @ 40 Sam Leong Road", "Class of 2014 Reunion", new DateTime(2024, 8, 17, 10, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "MembershipPlans",
                columns: new[] { "Id", "BuiltIn", "Description", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("7ad2dfda-82df-4597-a76f-40e5fd4fd28d"), true, "SSTAA EXCO", new TimeSpan(365, 0, 0, 0, 0), "EXCO", 0m },
                    { new Guid("c1869b12-56a9-4ed8-96d2-ef962c39799e"), true, "Ordinary", new TimeSpan(365, 0, 0, 0, 0), "Ordinary", 0m },
                    { new Guid("c28780c6-d687-4bb8-b9ce-5fbca1e347c2"), true, "All past/present staff and students who completed at least 1 year of study in SST but did not graduate", new TimeSpan(365, 0, 0, 0, 0), "Associate", 0m },
                    { new Guid("d258488b-c5a3-4f96-add7-366be4934900"), true, "All graduated alumni who are under 21", new TimeSpan(365, 0, 0, 0, 0), "Affiliate", 0m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirebaseId", "Name" },
                values: new object[,]
                {
                    { new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), "tan_zheng_jie@sstaa.org", "5ZPERFPTvfMfxwhH7SGsOmXqSco2", "Tan Zheng Jie" },
                    { new Guid("a78a112f-3355-499e-aafd-824c14858b34"), "guardhouse+laptop@sstaa.org", "EGjzDDZgHxXd80aGUYkeoP5fNnC2", "Guard House Laptop" },
                    { new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), "qinguan20040914@gmail.com", "GuZZVeOdlhNsf5dZGQmU2yV1Ox33", "Qin Guan" }
                });

            migrationBuilder.InsertData(
                table: "Attendees",
                columns: new[] { "Id", "AdmittedAt", "AdmittedById", "EventId", "UserId" },
                values: new object[,]
                {
                    { new Guid("21ace349-7df6-49af-a204-9581c6cf017b"), null, null, new Guid("f2c84690-0311-4563-9635-ad0982cc1229"), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631") },
                    { new Guid("66e832ab-e00e-48aa-ba66-cb62f5dab6c0"), null, null, new Guid("535bb727-f3eb-4cb3-adcf-aef04f14e82a"), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631") }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "MemberId" },
                values: new object[,]
                {
                    { new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), "EXCO-2" },
                    { new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), "EXCO-1" }
                });

            migrationBuilder.InsertData(
                table: "ServiceAccounts",
                columns: new[] { "Id", "ServiceAccountType" },
                values: new object[] { new Guid("a78a112f-3355-499e-aafd-824c14858b34"), "GuardHouse" });

            migrationBuilder.InsertData(
                table: "AlumniMembers",
                columns: new[] { "Id", "GraduationYear" },
                values: new object[,]
                {
                    { new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), null },
                    { new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), 2000 }
                });

            migrationBuilder.InsertData(
                table: "CheckIns",
                columns: new[] { "Id", "CheckInDateTime", "CheckOutDateTime", "ServiceAccountId" },
                values: new object[,]
                {
                    { new Guid("4f770e07-4f69-402d-9b1a-5e26e7f822f2"), new DateTime(2024, 8, 17, 10, 20, 0, 0, DateTimeKind.Utc), null, new Guid("a78a112f-3355-499e-aafd-824c14858b34") },
                    { new Guid("d96a21d0-81dc-4d65-aa7a-020af478a849"), new DateTime(2024, 8, 17, 10, 15, 0, 0, DateTimeKind.Utc), null, new Guid("a78a112f-3355-499e-aafd-824c14858b34") },
                    { new Guid("e15ae42b-b986-4fc6-b116-e7db6b213339"), new DateTime(2024, 8, 17, 10, 30, 0, 0, DateTimeKind.Utc), null, new Guid("a78a112f-3355-499e-aafd-824c14858b34") }
                });

            migrationBuilder.InsertData(
                table: "MembershipSubscriptions",
                columns: new[] { "Id", "EndDateTime", "MemberId", "MembershipPlanId", "PaymentIntentId", "PaymentIntentState", "StartDateTime" },
                values: new object[,]
                {
                    { new Guid("58352738-955f-41b5-ae42-57c2e01d7452"), new DateTime(2024, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), new Guid("7ad2dfda-82df-4597-a76f-40e5fd4fd28d"), null, null, new DateTime(2023, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d44eba3b-5556-4978-8188-7440762b1288"), new DateTime(2024, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), new Guid("7ad2dfda-82df-4597-a76f-40e5fd4fd28d"), null, null, new DateTime(2023, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "GuestCheckIns",
                columns: new[] { "Id", "Name", "Nric", "Phone", "Reason" },
                values: new object[] { new Guid("e15ae42b-b986-4fc6-b116-e7db6b213339"), "Alex", "999B", "9999 9999", "Alex is bored" });

            migrationBuilder.InsertData(
                table: "UserCheckIns",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { new Guid("4f770e07-4f69-402d-9b1a-5e26e7f822f2"), new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f") },
                    { new Guid("d96a21d0-81dc-4d65-aa7a-020af478a849"), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_AdmittedById",
                table: "Attendees",
                column: "AdmittedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_UserId",
                table: "Attendees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_ServiceAccountId",
                table: "CheckIns",
                column: "ServiceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_MembersId",
                table: "GroupMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberId",
                table: "Members",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipSubscriptionPayment_MembershipSubscriptionId",
                table: "MembershipSubscriptionPayment",
                column: "MembershipSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipSubscriptions_MemberId",
                table: "MembershipSubscriptions",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipSubscriptions_MembershipPlanId",
                table: "MembershipSubscriptions",
                column: "MembershipPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCheckIns_UserId",
                table: "UserCheckIns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirebaseId",
                table: "Users",
                column: "FirebaseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumniMembers");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "EmployeeMembers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "GuestCheckIns");

            migrationBuilder.DropTable(
                name: "MembershipSubscriptionPayment");

            migrationBuilder.DropTable(
                name: "SystemAdmins");

            migrationBuilder.DropTable(
                name: "UserCheckIns");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "MembershipSubscriptions");

            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MembershipPlans");

            migrationBuilder.DropTable(
                name: "ServiceAccounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
