using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SSTAlumniAssociation.Core.Entities;

#nullable disable

namespace SSTAlumniAssociation.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedMembershipPaymentIntentState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58352738-955f-41b5-ae42-57c2e01d7452"),
                column: "PaymentIntentState",
                value: PaymentIntentState.Success);

            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d44eba3b-5556-4978-8188-7440762b1288"),
                column: "PaymentIntentState",
                value: PaymentIntentState.Success);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58352738-955f-41b5-ae42-57c2e01d7452"),
                column: "PaymentIntentState",
                value: PaymentIntentState.None);

            migrationBuilder.UpdateData(
                table: "MembershipSubscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d44eba3b-5556-4978-8188-7440762b1288"),
                column: "PaymentIntentState",
                value: PaymentIntentState.None);
        }
    }
}
