using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThMoCo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDispatchToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DispatchDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDispatched",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8632), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8721) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8729), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8732) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 12, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8738), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8741) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8746), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8750) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8756), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8759) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8764), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8767) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8772), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8776) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8781), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8785) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8790), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8794) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 5, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8800), new DateTime(2025, 1, 7, 11, 19, 54, 282, DateTimeKind.Local).AddTicks(8803) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DispatchDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDispatched",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4019), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4104) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 10, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4109), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4111) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 12, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4114), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4116) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4119), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4121) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 8, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4126), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4128) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 9, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4131), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4133) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4136), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4137) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4140), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4142) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 6, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4145), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4147) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 5, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4150), new DateTime(2025, 1, 3, 14, 24, 12, 929, DateTimeKind.Local).AddTicks(4152) });
        }
    }
}
