using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PromoChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "code",
                table: "promo_code",
                newName: "code1");

            migrationBuilder.UpdateData(
                table: "promo_code",
                keyColumn: "id",
                keyValue: new Guid("2d5c0b24-0f61-4ae3-ad2a-e0ded5153d09"),
                columns: new[] { "begin_date", "end_date" },
                values: new object[] { new DateTime(2024, 4, 20, 16, 31, 32, 283, DateTimeKind.Local).AddTicks(3996), new DateTime(2024, 4, 21, 16, 31, 32, 283, DateTimeKind.Local).AddTicks(4009) });

            migrationBuilder.UpdateData(
                table: "promo_code",
                keyColumn: "id",
                keyValue: new Guid("33867eef-321b-4b88-a4e6-e57f77e3e57a"),
                columns: new[] { "begin_date", "end_date" },
                values: new object[] { new DateTime(2024, 4, 19, 16, 31, 32, 283, DateTimeKind.Local).AddTicks(4022), new DateTime(2024, 4, 22, 16, 31, 32, 283, DateTimeKind.Local).AddTicks(4023) });

            migrationBuilder.UpdateData(
                table: "promo_code",
                keyColumn: "id",
                keyValue: new Guid("fd49d9a9-609f-4376-bfb4-14b157aae3a9"),
                columns: new[] { "begin_date", "end_date" },
                values: new object[] { new DateTime(2024, 4, 17, 16, 31, 32, 283, DateTimeKind.Local).AddTicks(4029), new DateTime(2024, 4, 24, 16, 31, 32, 283, DateTimeKind.Local).AddTicks(4030) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "code1",
                table: "promo_code",
                newName: "code");

            migrationBuilder.UpdateData(
                table: "promo_code",
                keyColumn: "id",
                keyValue: new Guid("2d5c0b24-0f61-4ae3-ad2a-e0ded5153d09"),
                columns: new[] { "begin_date", "end_date" },
                values: new object[] { new DateTime(2024, 4, 20, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8925), new DateTime(2024, 4, 21, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8938) });

            migrationBuilder.UpdateData(
                table: "promo_code",
                keyColumn: "id",
                keyValue: new Guid("33867eef-321b-4b88-a4e6-e57f77e3e57a"),
                columns: new[] { "begin_date", "end_date" },
                values: new object[] { new DateTime(2024, 4, 19, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8950), new DateTime(2024, 4, 22, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8951) });

            migrationBuilder.UpdateData(
                table: "promo_code",
                keyColumn: "id",
                keyValue: new Guid("fd49d9a9-609f-4376-bfb4-14b157aae3a9"),
                columns: new[] { "begin_date", "end_date" },
                values: new object[] { new DateTime(2024, 4, 17, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8956), new DateTime(2024, 4, 24, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8957) });
        }
    }
}
