using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persontec.Api.Migrations
{
    public partial class Transfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndingDate",
                table: "EmploymentPeriod",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    TransferId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartingDate = table.Column<DateTime>(nullable: false),
                    EndingDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    CurrentOrganizationOrganizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.TransferId);
                    table.ForeignKey(
                        name: "FK_Transfers_Organizations_CurrentOrganizationOrganizationId",
                        column: x => x.CurrentOrganizationOrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Transfers",
                columns: new[] { "TransferId", "CurrentOrganizationOrganizationId", "EmployeeId", "EndingDate", "StartingDate" },
                values: new object[] { 1, null, 1, new DateTime(2020, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Transfers",
                columns: new[] { "TransferId", "CurrentOrganizationOrganizationId", "EmployeeId", "EndingDate", "StartingDate" },
                values: new object[] { 2, null, 1, new DateTime(2014, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_CurrentOrganizationOrganizationId",
                table: "Transfers",
                column: "CurrentOrganizationOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_EmployeeId",
                table: "Transfers",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndingDate",
                table: "EmploymentPeriod",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
