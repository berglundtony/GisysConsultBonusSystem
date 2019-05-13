using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GisysConsultBonusSystem.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusInMoney",
                table: "Bonus");

            migrationBuilder.RenameColumn(
                name: "TotalHoursWithBonus",
                table: "Bonus",
                newName: "PeriodOfEmployment");

            migrationBuilder.AlterColumn<int>(
                name: "NetResult",
                table: "Bonus",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BonusDate",
                table: "Bonus",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodOfEmployment",
                table: "Bonus",
                newName: "TotalHoursWithBonus");

            migrationBuilder.AlterColumn<decimal>(
                name: "NetResult",
                table: "Bonus",
                type: "decimal(18, 0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BonusDate",
                table: "Bonus",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<decimal>(
                name: "BonusInMoney",
                table: "Bonus",
                type: "decimal(18, 0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
