using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InforseTestTask.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("45cff320-1b71-42a5-82ca-08405c914730"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fc9fafb1-f3b2-4bb2-8124-e6edcbf7ba8c"));

            migrationBuilder.InsertData(
                table: "AboutInfos",
                columns: new[] { "Id", "Description", "LastUpdated" },
                values: new object[] { 1L, "The ShortenUrl method is a utility function that enables the transformation of a long, complex URL into a compact, easy-to-share shortened version. It utilizes a hashing technique to generate a unique identifier for the provided URL and then converts it into a shortened format, which can be shared or stored more easily. This method is ideal for applications that require a simplified URL system for linking purposes, particularly in cases where space is limited or for creating cleaner, more user-friendly links.", new DateTime(2025, 1, 18, 11, 24, 17, 509, DateTimeKind.Utc).AddTicks(3327) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4f12fa3d-18b9-45ec-8163-f0c2f6e63b2b"), "4f12fa3d-18b9-45ec-8163-f0c2f6e63b2b", "User", "USER" },
                    { new Guid("91fd4891-dbd2-4848-86cc-d6e0d3716723"), "91fd4891-dbd2-4848-86cc-d6e0d3716723", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_OriginalUrl",
                table: "ShortUrls",
                column: "OriginalUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShortUrls_OriginalUrl",
                table: "ShortUrls");

            migrationBuilder.DeleteData(
                table: "AboutInfos",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4f12fa3d-18b9-45ec-8163-f0c2f6e63b2b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("91fd4891-dbd2-4848-86cc-d6e0d3716723"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("45cff320-1b71-42a5-82ca-08405c914730"), "45cff320-1b71-42a5-82ca-08405c914730", "Admin", "ADMIN" },
                    { new Guid("fc9fafb1-f3b2-4bb2-8124-e6edcbf7ba8c"), "fc9fafb1-f3b2-4bb2-8124-e6edcbf7ba8c", "User", "USER" }
                });
        }
    }
}
