using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class upadte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWatcher_Lectures_LectureId",
                table: "UserWatcher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWatcher",
                table: "UserWatcher");

            migrationBuilder.RenameTable(
                name: "UserWatcher",
                newName: "UserWatchers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWatchers",
                table: "UserWatchers",
                columns: new[] { "LectureId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchers_Lectures_LectureId",
                table: "UserWatchers",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchers_Lectures_LectureId",
                table: "UserWatchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWatchers",
                table: "UserWatchers");

            migrationBuilder.RenameTable(
                name: "UserWatchers",
                newName: "UserWatcher");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWatcher",
                table: "UserWatcher",
                columns: new[] { "LectureId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatcher_Lectures_LectureId",
                table: "UserWatcher",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
