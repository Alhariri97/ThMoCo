using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThMoCo.Api.Migrations
{
    /// <inheritdoc />
    public partial class cvvToStringNotInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cvv",
                table: "PaymentCards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5093), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5155) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5159), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5161) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 12, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5165), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5166) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5169), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5172), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5174) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5176), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5177) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5180), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5181) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5183), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5185) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5187), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 5, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5191), new DateTime(2025, 1, 3, 11, 31, 55, 325, DateTimeKind.Local).AddTicks(5192) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cvv",
                table: "PaymentCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
