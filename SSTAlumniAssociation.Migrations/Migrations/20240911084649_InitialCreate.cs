using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SSTAlumniAssociation.WebApi.Migrations
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
                    Description = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
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
                    MemberId = table.Column<string>(type: "text", nullable: false),
                    Membership = table.Column<string>(type: "text", nullable: false)
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
                    { new Guid("1710e6a9-f8e5-4456-b5a4-3c7d8055159f"), false, "https://picsum.photos/200", "A very warm homecoming.", new DateTime(2024, 9, 21, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9940), "SST Multi-Purpose Hall", "Trial", new DateTime(2024, 9, 11, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9940), null },
                    { new Guid("bc7016f8-2f9d-4057-a3ae-58828d66522c"), true, "https://picsum.photos/200", "A very warm homecoming.", new DateTime(2024, 9, 21, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9920), "SST Multi-Purpose Hall", "Homecoming 2024", new DateTime(2024, 9, 11, 8, 46, 48, 690, DateTimeKind.Utc).AddTicks(9920), null }
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
                    { new Guid("105d9b12-a084-4417-bf68-742e33bab9d9"), null, null, new Guid("bc7016f8-2f9d-4057-a3ae-58828d66522c"), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631") },
                    { new Guid("66cd9cb6-2ed1-480d-bb1d-f4d1680177f5"), null, null, new Guid("1710e6a9-f8e5-4456-b5a4-3c7d8055159f"), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631") }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "MemberId", "Membership" },
                values: new object[,]
                {
                    { new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), "EXCO-2", "Exco" },
                    { new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), "EXCO-1", "Exco" }
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
                    { new Guid("3dc67aff-20d5-4299-a251-f413f0a8f4d0"), new DateTime(2024, 9, 11, 8, 46, 48, 691, DateTimeKind.Utc).AddTicks(60), null, new Guid("a78a112f-3355-499e-aafd-824c14858b34") },
                    { new Guid("696b775b-effa-46fd-880e-5a9c34d0966c"), new DateTime(2024, 9, 11, 8, 46, 48, 691, DateTimeKind.Utc).AddTicks(80), null, new Guid("a78a112f-3355-499e-aafd-824c14858b34") },
                    { new Guid("732faab7-22e4-406b-9339-915d223ac2f2"), new DateTime(2024, 9, 11, 8, 46, 48, 691, DateTimeKind.Utc).AddTicks(40), null, new Guid("a78a112f-3355-499e-aafd-824c14858b34") }
                });

            migrationBuilder.InsertData(
                table: "GuestCheckIns",
                columns: new[] { "Id", "Name", "Nric", "Phone", "Reason" },
                values: new object[] { new Guid("696b775b-effa-46fd-880e-5a9c34d0966c"), "Alex", "999B", "9999 9999", "Alex is bored" });

            migrationBuilder.InsertData(
                table: "UserCheckIns",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { new Guid("3dc67aff-20d5-4299-a251-f413f0a8f4d0"), new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f") },
                    { new Guid("732faab7-22e4-406b-9339-915d223ac2f2"), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631") }
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
                name: "MembershipSubscriptions");

            migrationBuilder.DropTable(
                name: "SystemAdmins");

            migrationBuilder.DropTable(
                name: "UserCheckIns");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MembershipPlans");

            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.DropTable(
                name: "ServiceAccounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
