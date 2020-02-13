using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persontec.Api.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    EmployeeNumber = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    SupervisorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentPeriod",
                columns: table => new
                {
                    EmploymentPeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartingDate = table.Column<DateTime>(nullable: false),
                    EndingDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentPeriod", x => x.EmploymentPeriodId);
                    table.ForeignKey(
                        name: "FK_EmploymentPeriod_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(nullable: false),
                    OrganizationName = table.Column<string>(maxLength: 255, nullable: false),
                    OrganizationCode = table.Column<int>(nullable: false),
                    VicePresidentEmployeeId = table.Column<int>(nullable: true),
                    VicePresidentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organizations_Employees_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Organizations_Employees_VicePresidentEmployeeId",
                        column: x => x.VicePresidentEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeNumber", "FirstName", "LastName", "OrganizationId", "SupervisorId" },
                values: new object[] { 1, 101, "Dennis", "Dunaway", 1, null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeNumber", "FirstName", "LastName", "OrganizationId", "SupervisorId" },
                values: new object[] { 2, 115, "James", "Smith", 1, 1 });

            migrationBuilder.InsertData(
                table: "EmploymentPeriod",
                columns: new[] { "EmploymentPeriodId", "EmployeeId", "EndingDate", "StartingDate", "Status" },
                values: new object[] { 1, 1, new DateTime(2020, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "OrganizationCode", "OrganizationName", "VicePresidentEmployeeId", "VicePresidentId" },
                values: new object[] { 1, 12345, "Group J", null, 1 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeNumber", "FirstName", "LastName", "OrganizationId", "SupervisorId" },
                values: new object[] { 3, 1016, "Jake ", "Dwight", 1, 2 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeNumber", "FirstName", "LastName", "OrganizationId", "SupervisorId" },
                values: new object[] { 4, 1010, "Tim", "Tunney", 1, 2 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeNumber", "FirstName", "LastName", "OrganizationId", "SupervisorId" },
                values: new object[] { 5, 102, "Alec", "Smart", 1, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentPeriod_EmployeeId",
                table: "EmploymentPeriod",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_VicePresidentEmployeeId",
                table: "Organizations",
                column: "VicePresidentEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmploymentPeriod");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
