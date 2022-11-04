using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    public partial class removeUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeletedItems_Users_UserId",
                table: "DeletedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UrlDatas_Users_UserId",
                table: "UrlDatas");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UrlDatas_UserId",
                table: "UrlDatas");

            migrationBuilder.DropIndex(
                name: "IX_DeletedItems_UserId",
                table: "DeletedItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UrlDatas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DeletedItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UrlDatas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DeletedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlDatas_UserId",
                table: "UrlDatas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedItems_UserId",
                table: "DeletedItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeletedItems_Users_UserId",
                table: "DeletedItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UrlDatas_Users_UserId",
                table: "UrlDatas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
