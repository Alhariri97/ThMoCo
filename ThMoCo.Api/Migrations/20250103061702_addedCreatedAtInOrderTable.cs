using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThMoCo.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedCreatedAtInOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3296), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3370) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3377), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3381) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 12, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3387), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3390) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3394), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3397) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3401), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3404) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3408), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3411) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3415), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3418) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3421), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3424) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3428), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3431) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 5, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3435), new DateTime(2025, 1, 3, 6, 17, 1, 585, DateTimeKind.Local).AddTicks(3438) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

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
    }
}
