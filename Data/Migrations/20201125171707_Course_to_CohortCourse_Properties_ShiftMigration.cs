using Microsoft.EntityFrameworkCore.Migrations;

namespace AZLearn.Data.Migrations
{
    public partial class Course_to_CohortCourse_Properties_ShiftMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Instructor",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "FK_Course_Instructor",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ResourcesLink",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "CohortCourse",
                type: "int(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResourcesLink",
                table: "CohortCourse",
                type: "varchar(250)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_CohortCourse_Instructor",
                table: "CohortCourse",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortCourse_Instructor",
                table: "CohortCourse",
                column: "InstructorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortCourse_Instructor",
                table: "CohortCourse");

            migrationBuilder.DropIndex(
                name: "FK_CohortCourse_Instructor",
                table: "CohortCourse");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "CohortCourse");

            migrationBuilder.DropColumn(
                name: "ResourcesLink",
                table: "CohortCourse");

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Course",
                type: "int(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResourcesLink",
                table: "Course",
                type: "varchar(250)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_Course_Instructor",
                table: "Course",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Instructor",
                table: "Course",
                column: "InstructorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
