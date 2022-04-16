using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndProject.Migrations
{
    public partial class ChangeImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyProperty_Students_StudentId",
                table: "MyProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty");

            migrationBuilder.RenameTable(
                name: "MyProperty",
                newName: "StudentImages");

            migrationBuilder.RenameIndex(
                name: "IX_MyProperty_StudentId",
                table: "StudentImages",
                newName: "IX_StudentImages_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentImages",
                table: "StudentImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentImages_Students_StudentId",
                table: "StudentImages",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentImages_Students_StudentId",
                table: "StudentImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentImages",
                table: "StudentImages");

            migrationBuilder.RenameTable(
                name: "StudentImages",
                newName: "MyProperty");

            migrationBuilder.RenameIndex(
                name: "IX_StudentImages_StudentId",
                table: "MyProperty",
                newName: "IX_MyProperty_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MyProperty_Students_StudentId",
                table: "MyProperty",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
