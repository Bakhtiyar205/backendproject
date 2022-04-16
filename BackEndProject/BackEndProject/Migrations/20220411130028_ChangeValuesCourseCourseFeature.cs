using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class ChangeValuesCourseCourseFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseFeatures_CourseFeatureId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseFeatureId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseFeatureId",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseFeatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseFeatures");

            migrationBuilder.AddColumn<int>(
                name: "CourseFeatureId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseFeatureId",
                table: "Courses",
                column: "CourseFeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseFeatures_CourseFeatureId",
                table: "Courses",
                column: "CourseFeatureId",
                principalTable: "CourseFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
