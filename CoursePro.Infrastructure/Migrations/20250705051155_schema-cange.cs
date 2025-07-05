using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursePro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class schemacange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "User",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Enrollments",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CourseSections",
                newName: "CourseSections",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Courses",
                newSchema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "User",
                schema: "dbo",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                schema: "dbo",
                newName: "Enrollments");

            migrationBuilder.RenameTable(
                name: "CourseSections",
                schema: "dbo",
                newName: "CourseSections");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "dbo",
                newName: "Courses");
        }
    }
}
