using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SSTAlumniAssociation.Core.Entities;

#nullable disable

namespace SSTAlumniAssociation.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlumniMembers",
                keyColumn: "Id",
                keyValue: new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"));

            migrationBuilder.DeleteData(
                table: "AlumniMembers",
                keyColumn: "Id",
                keyValue: new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"));

            migrationBuilder.DeleteData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58352738-955f-41b5-ae42-57c2e01d7452"));

            migrationBuilder.DeleteData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d44eba3b-5556-4978-8188-7440762b1288"));

            migrationBuilder.DeleteData(
                table: "UserCheckIns",
                keyColumn: "Id",
                keyValue: new Guid("4f770e07-4f69-402d-9b1a-5e26e7f822f2"));

            migrationBuilder.DeleteData(
                table: "CheckIns",
                keyColumn: "Id",
                keyValue: new Guid("4f770e07-4f69-402d-9b1a-5e26e7f822f2"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Members",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "MailingAddress",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferredName",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SstEmail",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SstFirebaseId",
                table: "Members",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ManualMemberApprovals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualMemberApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualMemberApprovals_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManualMemberApprovals_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SystemAdmins",
                column: "Id",
                value: new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"));

            migrationBuilder.CreateIndex(
                name: "IX_ManualMemberApprovals_ApproverId",
                table: "ManualMemberApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualMemberApprovals_MemberId",
                table: "ManualMemberApprovals",
                column: "MemberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManualMemberApprovals");

            migrationBuilder.DeleteData(
                table: "SystemAdmins",
                keyColumn: "Id",
                keyValue: new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"));

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MailingAddress",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PreferredName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SstEmail",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SstFirebaseId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "Members");

            migrationBuilder.InsertData(
                table: "CheckIns",
                columns: new[] { "Id", "CheckInDateTime", "CheckOutDateTime", "ServiceAccountId" },
                values: new object[] { new Guid("4f770e07-4f69-402d-9b1a-5e26e7f822f2"), new DateTime(2024, 8, 17, 10, 20, 0, 0, DateTimeKind.Utc), null, new Guid("a78a112f-3355-499e-aafd-824c14858b34") });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "MemberId" },
                values: new object[] { new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), "EXCO-1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirebaseId", "Name" },
                values: new object[] { new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), "tan_zheng_jie@sstaa.org", "5ZPERFPTvfMfxwhH7SGsOmXqSco2", "Tan Zheng Jie" });

            migrationBuilder.InsertData(
                table: "AlumniMembers",
                columns: new[] { "Id", "GraduationYear" },
                values: new object[] { new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), 2000 });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "MemberId" },
                values: new object[] { new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), "EXCO-2" });

            migrationBuilder.InsertData(
                table: "MembershipSubscriptions",
                columns: new[] { "Id", "EndDateTime", "MemberId", "MembershipPlanId", "PaymentIntentId", "PaymentIntentState", "StartDateTime" },
                values: new object[] { new Guid("58352738-955f-41b5-ae42-57c2e01d7452"), new DateTime(2026, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("df90f5ea-a236-413f-a6c1-ca9197427631"), new Guid("7ad2dfda-82df-4597-a76f-40e5fd4fd28d"), null, PaymentIntentState.Success, new DateTime(2023, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "UserCheckIns",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("4f770e07-4f69-402d-9b1a-5e26e7f822f2"), new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f") });

            migrationBuilder.InsertData(
                table: "AlumniMembers",
                columns: new[] { "Id", "GraduationYear" },
                values: new object[] { new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), null });

            migrationBuilder.InsertData(
                table: "MembershipSubscriptions",
                columns: new[] { "Id", "EndDateTime", "MemberId", "MembershipPlanId", "PaymentIntentId", "PaymentIntentState", "StartDateTime" },
                values: new object[] { new Guid("d44eba3b-5556-4978-8188-7440762b1288"), new DateTime(2026, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("829bc4dc-2d8f-46df-acbb-c52c0e7f958f"), new Guid("7ad2dfda-82df-4597-a76f-40e5fd4fd28d"), null, PaymentIntentState.Success, new DateTime(2023, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
