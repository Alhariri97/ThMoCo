using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThMoCo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedDate", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "StockQuantity", "Supplier", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2024, 6, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(63), "A high-performance laptop for work and gaming.", "http://example.com/laptop.jpg", true, "Laptop", 999.99m, 10, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(135) },
                    { 2, "Electronics", new DateTime(2024, 9, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(141), "A modern smartphone with excellent camera quality.", "http://example.com/smartphone.jpg", true, "Smartphone", 799.99m, 20, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(143) },
                    { 3, "Accessories", new DateTime(2024, 11, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(147), "Noise-canceling headphones for immersive sound.", "http://example.com/headphones.jpg", true, "Headphones", 199.99m, 50, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(150) },
                    { 4, "Electronics", new DateTime(2024, 10, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(154), "A 24-inch monitor with stunning picture quality.", "http://example.com/monitor.jpg", true, "Monitor", 299.99m, 5, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(156) },
                    { 5, "Electronics", new DateTime(2024, 7, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(160), "A lightweight tablet, perfect for reading and browsing.", "http://example.com/tablet.jpg", true, "Tablet", 499.99m, 15, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(162) },
                    { 6, "Furniture", new DateTime(2024, 8, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(166), "Ergonomic gaming chair for extended comfort.", "http://example.com/gamingchair.jpg", true, "Gaming Chair", 199.99m, 25, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(168) },
                    { 7, "Accessories", new DateTime(2024, 6, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(172), "Mechanical keyboard with customizable RGB lighting.", "http://example.com/keyboard.jpg", true, "Keyboard", 89.99m, 30, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(174) },
                    { 8, "Accessories", new DateTime(2024, 10, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(178), "A wireless mouse with high precision and long battery life.", "http://example.com/mouse.jpg", true, "Wireless Mouse", 49.99m, 40, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(180) },
                    { 9, "Electronics", new DateTime(2024, 5, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(184), "A stylish smartwatch with health tracking features.", "http://example.com/smartwatch.jpg", true, "Smartwatch", 299.99m, 10, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(186) },
                    { 10, "Accessories", new DateTime(2024, 4, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(189), "A 1TB external hard drive for backups and storage.", "http://example.com/harddrive.jpg", true, "External Hard Drive", 149.99m, 20, null, new DateTime(2024, 12, 20, 0, 18, 29, 932, DateTimeKind.Local).AddTicks(191) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
