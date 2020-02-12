using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persontec.Api.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeNumber", "FirstName", "LastName", "SupervisorEmployeeId" },
                values: new object[] { 1, 101, "Dennis", "Dunaway", null });

            migrationBuilder.InsertData(
                table: "EmploymentPeriod",
                columns: new[] { "EmploymentPeriodId", "EmployeeId", "EndingDate", "StartingDate", "Status" },
                values: new object[] { 1, 1, new DateTime(2020, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "OrganizationCode", "OrganizationName", "VicePresidentId" },
                values: new object[] { 1, 12345, "Group J", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmploymentPeriod",
                keyColumn: "EmploymentPeriodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);
        }
    }
}
