using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSTAlumniAssociation.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedMembershipDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58352738-955f-41b5-ae42-57c2e01d7452"),
                column: "EndDateTime",
                value: new DateTime(2026, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d44eba3b-5556-4978-8188-7440762b1288"),
                column: "EndDateTime",
                value: new DateTime(2026, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58352738-955f-41b5-ae42-57c2e01d7452"),
                column: "EndDateTime",
                value: new DateTime(2024, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d44eba3b-5556-4978-8188-7440762b1288"),
                column: "EndDateTime",
                value: new DateTime(2024, 12, 31, 16, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
