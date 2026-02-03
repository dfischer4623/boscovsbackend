using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoscovsBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    employee_number = table.Column<string>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: false),
                    department = table.Column<string>(type: "TEXT", nullable: false),
                    job_title = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "id", "created_at", "department", "employee_number", "first_name", "job_title", "last_name", "status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 3, 17, 50, 34, 986, DateTimeKind.Local).AddTicks(251), "IT", "1001", "Mark", "Developer", "Fischer", "Active" },
                    { 2, new DateTime(2026, 2, 3, 17, 50, 34, 986, DateTimeKind.Local).AddTicks(255), "Sales", "1002", "John", "Manager", "Doe", "Active" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "access", "created_at", "email", "password", "status", "updated_at", "username" },
                values: new object[] { 1, "admin", "2026-02-03 17:50:34", "admin@boscovs.com", "$2a$11$GFhmqt6w.rHc3Htj66tNa.elY54RTEgtq1SWsCjx24vsw8P395NHS", "Active", "2026-02-03 17:50:34", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
