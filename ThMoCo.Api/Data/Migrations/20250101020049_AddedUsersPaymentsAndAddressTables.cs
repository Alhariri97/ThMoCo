using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThMoCo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsersPaymentsAndAddressTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cvv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAuthId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fund = table.Column<double>(type: "float", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: true),
                    PaymentCardId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUsers_PaymentCards_PaymentCardId",
                        column: x => x.PaymentCardId,
                        principalTable: "PaymentCards",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedDate", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "StockQuantity", "Supplier", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2024, 7, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9590), "A high-performance laptop for work and gaming.", "http://example.com/laptop.jpg", true, "Laptop", 999.99m, 10, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9658) },
                    { 2, "Electronics", new DateTime(2024, 10, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9664), "A modern smartphone with excellent camera quality.", "http://example.com/smartphone.jpg", true, "Smartphone", 799.99m, 20, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9666) },
                    { 3, "Accessories", new DateTime(2024, 12, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9670), "Noise-canceling headphones for immersive sound.", "http://example.com/headphones.jpg", true, "Headphones", 199.99m, 50, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9672) },
                    { 4, "Electronics", new DateTime(2024, 11, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9676), "A 24-inch monitor with stunning picture quality.", "http://example.com/monitor.jpg", true, "Monitor", 299.99m, 5, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9679) },
                    { 5, "Electronics", new DateTime(2024, 8, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9683), "A lightweight tablet, perfect for reading and browsing.", "http://example.com/tablet.jpg", true, "Tablet", 499.99m, 15, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9685) },
                    { 6, "Furniture", new DateTime(2024, 9, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9688), "Ergonomic gaming chair for extended comfort.", "http://example.com/gamingchair.jpg", true, "Gaming Chair", 199.99m, 25, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9690) },
                    { 7, "Accessories", new DateTime(2024, 7, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9694), "Mechanical keyboard with customizable RGB lighting.", "http://example.com/keyboard.jpg", true, "Keyboard", 89.99m, 30, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9696) },
                    { 8, "Accessories", new DateTime(2024, 11, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9700), "A wireless mouse with high precision and long battery life.", "http://example.com/mouse.jpg", true, "Wireless Mouse", 49.99m, 40, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9702) },
                    { 9, "Electronics", new DateTime(2024, 6, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9705), "A stylish smartwatch with health tracking features.", "http://example.com/smartwatch.jpg", true, "Smartwatch", 299.99m, 10, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9707) },
                    { 10, "Accessories", new DateTime(2024, 5, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9711), "A 1TB external hard drive for backups and storage.", "http://example.com/harddrive.jpg", true, "External Hard Drive", 149.99m, 20, null, new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9714) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AddressId",
                table: "AppUsers",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PaymentCardId",
                table: "AppUsers",
                column: "PaymentCardId",
                unique: true,
                filter: "[PaymentCardId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "PaymentCards");
        }
    }
}
