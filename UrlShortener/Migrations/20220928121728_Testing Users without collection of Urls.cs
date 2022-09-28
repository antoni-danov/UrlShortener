using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Migrations
{
    public partial class TestingUserswithoutcollectionofUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlDatas_Users_UserId",
                table: "UrlDatas");

            migrationBuilder.DropIndex(
                name: "IX_UrlDatas_UserId",
                table: "UrlDatas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UrlDatas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UrlDatas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrlDatas_UserId",
                table: "UrlDatas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlDatas_Users_UserId",
                table: "UrlDatas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
