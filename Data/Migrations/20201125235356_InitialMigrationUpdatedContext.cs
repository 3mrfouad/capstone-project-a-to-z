using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AZLearn.Migrations
{
    public partial class InitialMigrationUpdatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cohort",
                columns: table => new
                {
                    CohortId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Capacity = table.Column<int>(type: "int(3)", nullable: true),
                    ModeOfTeaching = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    City = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohort", x => x.CohortId);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    DurationHrs = table.Column<float>(type: "float(5,2)", nullable: false),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CohortId = table.Column<int>(type: "int(10)", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    PasswordHash = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    IsInstructor = table.Column<bool>(type: "boolean", nullable: false),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Cohort",
                        column: x => x.CohortId,
                        principalTable: "Cohort",
                        principalColumn: "CohortId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CohortCourse",
                columns: table => new
                {
                    CohortId = table.Column<int>(type: "int(10)", nullable: false),
                    CourseId = table.Column<int>(type: "int(10)", nullable: false),
                    InstructorId = table.Column<int>(type: "int(10)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    ResourcesLink = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CohortCourse", x => new { x.CohortId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_CohortCourse_Cohort_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohort",
                        principalColumn: "CohortId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CohortCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CohortCourse_Instructor",
                        column: x => x.InstructorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Homework",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(type: "int(10)", nullable: false),
                    CohortId = table.Column<int>(type: "int(10)", nullable: false),
                    InstructorId = table.Column<int>(type: "int(10)", nullable: false),
                    IsAssignment = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    AvgCompletionTime = table.Column<float>(type: "float(5,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DocumentLink = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    GitHubClassRoomLink = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homework", x => x.HomeworkId);
                    table.ForeignKey(
                        name: "FK_Homework_Cohort",
                        column: x => x.CohortId,
                        principalTable: "Cohort",
                        principalColumn: "CohortId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homework_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homework_Instructor",
                        column: x => x.InstructorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int(10)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_Student",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rubric",
                columns: table => new
                {
                    RubricId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HomeworkId = table.Column<int>(type: "int(10)", nullable: false),
                    IsChallenge = table.Column<bool>(type: "boolean", nullable: false),
                    Criteria = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Weight = table.Column<int>(type: "int(3)", nullable: false),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubric", x => x.RubricId);
                    table.ForeignKey(
                        name: "FK_Rubric_Homework",
                        column: x => x.HomeworkId,
                        principalTable: "Homework",
                        principalColumn: "HomeworkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoutOut",
                columns: table => new
                {
                    ShoutOutId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int(10)", nullable: false),
                    PeerId = table.Column<int>(type: "int(10)", nullable: false),
                    HomeworkId = table.Column<int>(type: "int(10)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    DurationMins = table.Column<float>(type: "float(5,2)", nullable: false),
                    Topic = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Comment = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoutOut", x => x.ShoutOutId);
                    table.ForeignKey(
                        name: "FK_ShoutOut_Homework",
                        column: x => x.HomeworkId,
                        principalTable: "Homework",
                        principalColumn: "HomeworkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoutOut_Peer",
                        column: x => x.PeerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoutOut_Student",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Timesheet",
                columns: table => new
                {
                    TimesheetId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HomeworkId = table.Column<int>(type: "int(10)", nullable: false),
                    StudentId = table.Column<int>(type: "int(10)", nullable: false),
                    SolvingTime = table.Column<float>(type: "float(5,2)", nullable: false),
                    StudyTime = table.Column<float>(type: "float(5,2)", nullable: false),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheet", x => x.TimesheetId);
                    table.ForeignKey(
                        name: "FK_Timesheet_Homework",
                        column: x => x.HomeworkId,
                        principalTable: "Homework",
                        principalColumn: "HomeworkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timesheet_Student",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    RubricId = table.Column<int>(type: "int(10)", nullable: false),
                    StudentId = table.Column<int>(type: "int(10)", nullable: false),
                    Mark = table.Column<int>(type: "int(3)", nullable: false),
                    InstructorComment = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    StudentComment = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Archive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => new { x.RubricId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_Grade_Rubric_RubricId",
                        column: x => x.RubricId,
                        principalTable: "Rubric",
                        principalColumn: "RubricId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grade_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CohortCourse_CourseId",
                table: "CohortCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "FK_CohortCourse_Instructor",
                table: "CohortCourse",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StudentId",
                table: "Grade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "FK_Homework_Cohort",
                table: "Homework",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "FK_Homework_Course",
                table: "Homework",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "FK_Homework_Instructor",
                table: "Homework",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "FK_Notification_Student",
                table: "Notification",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "FK_Rubric_Homework",
                table: "Rubric",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "FK_ShoutOut_Homework",
                table: "ShoutOut",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "FK_ShoutOut_Peer",
                table: "ShoutOut",
                column: "PeerId");

            migrationBuilder.CreateIndex(
                name: "FK_ShoutOut_Student",
                table: "ShoutOut",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "FK_Timesheet_Homework",
                table: "Timesheet",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "FK_Timesheet_Student",
                table: "Timesheet",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "FK_User_Cohort",
                table: "User",
                column: "CohortId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CohortCourse");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ShoutOut");

            migrationBuilder.DropTable(
                name: "Timesheet");

            migrationBuilder.DropTable(
                name: "Rubric");

            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cohort");
        }
    }
}
