using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GisysConsultBonusSystem.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    BonusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsultID = table.Column<int>(type: "int", nullable: false),
                    ChargedHours = table.Column<int>(type: "int", nullable: false),
                    ChargedHoursWithBonus = table.Column<int>(type: "int", nullable: false),
                    BonusPot = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    NetResult = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    TotalHoursWithBonus = table.Column<int>(type: "int", nullable: false),
                    BonusInMoney = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    BonusDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.BonusID);
                });

            migrationBuilder.CreateTable(
                name: "Consults",
                columns: table => new
                {
                    ConsultID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EmploymentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consults", x => x.ConsultID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "Consults");
        }
    }
}
