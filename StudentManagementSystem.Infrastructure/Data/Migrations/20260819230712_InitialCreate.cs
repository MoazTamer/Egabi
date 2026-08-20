using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagementSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.CheckConstraint("CK_Course_Credits", "[Credits] > 0");
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    GPA = table.Column<double>(type: "float(3)", precision: 3, scale: 2, nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.CheckConstraint("CK_Student_Age", "[Age] > 0");
                    table.CheckConstraint("CK_Student_GPA", "[GPA] >= 0 AND [GPA] <= 4");
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName", "Credits" },
                values: new object[,]
                {
                    { 1, "Database Systems", 3 },
                    { 2, "Object Oriented Programming", 3 },
                    { 3, "Web Development", 3 },
                    { 4, "Computer Networks", 3 },
                    { 5, "Software Engineering", 4 }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "FacultyName" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Engineering" },
                    { 3, "Business" },
                    { 4, "Medicine" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Age", "FacultyId", "GPA", "StudentName" },
                values: new object[,]
                {
                    { 1, 20, 1, 3.5, "Ahmed Ali" },
                    { 2, 21, 1, 3.7999999999999998, "Mohamed Hassan" },
                    { 3, 19, 1, 2.8999999999999999, "Sara Ahmed" },
                    { 4, 22, 2, 3.2000000000000002, "Omar Khaled" },
                    { 5, 21, 2, 3.7000000000000002, "Mariam Adel" },
                    { 6, 20, 3, 2.6000000000000001, "Youssef Samir" },
                    { 7, 22, 3, 3.3999999999999999, "Nour Mohamed" },
                    { 8, 23, 4, 3.8999999999999999, "Hana Mahmoud" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "EnrollmentDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 1, 3, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 5, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 6, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 7, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 1, 8, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 8, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentName",
                table: "Students",
                column: "StudentName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
