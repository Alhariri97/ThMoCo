using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThMoCo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
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
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Fund = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    { 1, "Electronics", new DateTime(2024, 7, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4019), "A high-performance laptop for work and gaming.", "http://example.com/laptop.jpg", true, "Laptop", 999.99m, 10, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4104) },
                    { 2, "Electronics", new DateTime(2024, 10, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4109), "A modern smartphone with excellent camera quality.", "http://example.com/smartphone.jpg", true, "Smartphone", 799.99m, 20, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4111) },
                    { 3, "Accessories", new DateTime(2024, 12, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4114), "Noise-canceling headphones for immersive sound.", "http://example.com/headphones.jpg", true, "Headphones", 199.99m, 50, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4116) },
                    { 4, "Electronics", new DateTime(2024, 11, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4119), "A 24-inch monitor with stunning picture quality.", "http://example.com/monitor.jpg", true, "Monitor", 299.99m, 5, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4121) },
                    { 5, "Electronics", new DateTime(2024, 8, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4126), "A lightweight tablet, perfect for reading and browsing.", "http://example.com/tablet.jpg", true, "Tablet", 499.99m, 15, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4128) },
                    { 6, "Furniture", new DateTime(2024, 9, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4131), "Ergonomic gaming chair for extended comfort.", "http://example.com/gamingchair.jpg", true, "Gaming Chair", 199.99m, 25, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4133) },
                    { 7, "Accessories", new DateTime(2024, 7, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4136), "Mechanical keyboard with customizable RGB lighting.", "http://example.com/keyboard.jpg", true, "Keyboard", 89.99m, 30, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4137) },
                    { 8, "Accessories", new DateTime(2024, 11, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4140), "A wireless mouse with high precision and long battery life.", "http://example.com/mouse.jpg", true, "Wireless Mouse", 49.99m, 40, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4142) },
                    { 9, "Electronics", new DateTime(2024, 6, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4145), "A stylish smartwatch with health tracking features.", "http://example.com/smartwatch.jpg", true, "Smartwatch", 299.99m, 10, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4147) },
                    { 10, "Accessories", new DateTime(2024, 5, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4150), "A 1TB external hard drive for backups and storage.", "http://example.com/harddrive.jpg", true, "External Hard Drive", 149.99m, 20, null, new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4152) }
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

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "PaymentCards");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
