using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_proc.Migrations
{
    public partial class answer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AskForInfoAnswerId",
                table: "AskForInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AskForInfoAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AskForInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AskForInfoAnswer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AskForInfo_AskForInfoAnswerId",
                table: "AskForInfo",
                column: "AskForInfoAnswerId",
                unique: true,
                filter: "[AskForInfoAnswerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AskForInfo_AskForInfoAnswer_AskForInfoAnswerId",
                table: "AskForInfo",
                column: "AskForInfoAnswerId",
                principalTable: "AskForInfoAnswer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AskForInfo_AskForInfoAnswer_AskForInfoAnswerId",
                table: "AskForInfo");

            migrationBuilder.DropTable(
                name: "AskForInfoAnswer");

            migrationBuilder.DropIndex(
                name: "IX_AskForInfo_AskForInfoAnswerId",
                table: "AskForInfo");

            migrationBuilder.DropColumn(
                name: "AskForInfoAnswerId",
                table: "AskForInfo");
        }
    }
}
