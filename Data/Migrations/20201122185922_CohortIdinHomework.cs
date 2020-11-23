using Microsoft.EntityFrameworkCore.Migrations;

namespace AZLearn.Data.Migrations
{
    public partial class CohortIdinHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CohortId",
                table: "Homework",
                type: "int(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Homework_CohortId",
                table: "Homework",
                column: "CohortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_Cohort_CohortId",
                table: "Homework",
                column: "CohortId",
                principalTable: "Cohort",
                principalColumn: "CohortId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homework_Cohort_CohortId",
                table: "Homework");

            migrationBuilder.DropIndex(
                name: "IX_Homework_CohortId",
                table: "Homework");

            migrationBuilder.DropColumn(
                name: "CohortId",
                table: "Homework");
        }
    }
}
