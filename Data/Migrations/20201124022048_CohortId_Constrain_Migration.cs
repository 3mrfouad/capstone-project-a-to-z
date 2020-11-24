using Microsoft.EntityFrameworkCore.Migrations;

namespace AZLearn.Data.Migrations
{
    public partial class CohortId_Constrain_Migration : Migration
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
                name: "FK_Homework_Cohort",
                table: "Homework",
                column: "CohortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homework_Cohort",
                table: "Homework",
                column: "CohortId",
                principalTable: "Cohort",
                principalColumn: "CohortId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homework_Cohort",
                table: "Homework");

            migrationBuilder.DropIndex(
                name: "FK_Homework_Cohort",
                table: "Homework");

            migrationBuilder.DropColumn(
                name: "CohortId",
                table: "Homework");
        }
    }
}
