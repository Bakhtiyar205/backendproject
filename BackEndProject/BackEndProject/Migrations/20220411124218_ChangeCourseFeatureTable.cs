using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class ChangeCourseFeatureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseDeatil",
                table: "CourseFeatures");

            migrationBuilder.AddColumn<string>(
                name: "Assesment",
                table: "CourseFeatures",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Hours",
                table: "CourseFeatures",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CourseFeatures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Monthes",
                table: "CourseFeatures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkillLevel",
                table: "CourseFeatures",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Starts",
                table: "CourseFeatures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StundentCount",
                table: "CourseFeatures",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assesment",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "Monthes",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "SkillLevel",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "Starts",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "StundentCount",
                table: "CourseFeatures");

            migrationBuilder.AddColumn<string>(
                name: "CourseDeatil",
                table: "CourseFeatures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
