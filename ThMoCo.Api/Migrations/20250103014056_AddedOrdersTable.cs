using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThMoCo.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cvv",
                table: "PaymentCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Fund",
                table: "AppUsers",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6467), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6524) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6528), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6529) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 12, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6533), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6535) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6537), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6538) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6541), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6542) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6544), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6545) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6548), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6549) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6551), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6552) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6555), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6556) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 5, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6558), new DateTime(2025, 1, 3, 1, 40, 55, 804, DateTimeKind.Local).AddTicks(6559) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Cvv",
                table: "PaymentCards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Fund",
                table: "AppUsers",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9590), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9658) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9664), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9666) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9670), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9672) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9676), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9679) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9683), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9685) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9688), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9694), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9696) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9700), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9702) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9705), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9707) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 5, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9711), new DateTime(2025, 1, 1, 2, 0, 49, 168, DateTimeKind.Local).AddTicks(9714) });
        }
    }
}
