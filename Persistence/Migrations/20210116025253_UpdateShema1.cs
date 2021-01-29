using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdateShema1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseStudy_Courses_CourseId",
                table: "CaseStudy");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_Section_SectionId",
                table: "Lecture");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Courses_CourseId",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecture",
                table: "Lecture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseStudy",
                table: "CaseStudy");

            migrationBuilder.RenameTable(
                name: "Section",
                newName: "Sections");

            migrationBuilder.RenameTable(
                name: "Lecture",
                newName: "Lectures");

            migrationBuilder.RenameTable(
                name: "CaseStudy",
                newName: "CaseStudies");

            migrationBuilder.RenameIndex(
                name: "IX_Section_CourseId",
                table: "Sections",
                newName: "IX_Sections_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Lecture_SectionId",
                table: "Lectures",
                newName: "IX_Lectures_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CaseStudy_CourseId",
                table: "CaseStudies",
                newName: "IX_CaseStudies_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lectures",
                table: "Lectures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseStudies",
                table: "CaseStudies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseStudies_Courses_CourseId",
                table: "CaseStudies",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Sections_SectionId",
                table: "Lectures",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseStudies_Courses_CourseId",
                table: "CaseStudies");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Sections_SectionId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lectures",
                table: "Lectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseStudies",
                table: "CaseStudies");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "Section");

            migrationBuilder.RenameTable(
                name: "Lectures",
                newName: "Lecture");

            migrationBuilder.RenameTable(
                name: "CaseStudies",
                newName: "CaseStudy");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CourseId",
                table: "Section",
                newName: "IX_Section_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Lectures_SectionId",
                table: "Lecture",
                newName: "IX_Lecture_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CaseStudies_CourseId",
                table: "CaseStudy",
                newName: "IX_CaseStudy_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                table: "Section",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecture",
                table: "Lecture",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseStudy",
                table: "CaseStudy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseStudy_Courses_CourseId",
                table: "CaseStudy",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_Section_SectionId",
                table: "Lecture",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Courses_CourseId",
                table: "Section",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
