using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxCalculator.Dal.Migrations
{
    public partial class InitialMigration_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TaxTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "TaxPeriod",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "TaxPeriod",
                keyColumn: "TaxPeriodId",
                keyValue: 7,
                column: "To",
                value: null);

            migrationBuilder.UpdateData(
                table: "TaxPeriod",
                keyColumn: "TaxPeriodId",
                keyValue: 8,
                column: "To",
                value: null);

            migrationBuilder.UpdateData(
                table: "TaxTypes",
                keyColumn: "TaxTypeId",
                keyValue: 1,
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TaxTypes",
                keyColumn: "TaxTypeId",
                keyValue: 2,
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TaxTypes",
                keyColumn: "TaxTypeId",
                keyValue: 3,
                column: "Order",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TaxTypes",
                keyColumn: "TaxTypeId",
                keyValue: 4,
                column: "Order",
                value: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "TaxTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "To",
                table: "TaxPeriod",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TaxPeriod",
                keyColumn: "TaxPeriodId",
                keyValue: 7,
                column: "To",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TaxPeriod",
                keyColumn: "TaxPeriodId",
                keyValue: 8,
                column: "To",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
