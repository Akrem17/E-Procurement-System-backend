using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_proc.Migrations
{
    public partial class @null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AskForInfo_AskForInfoAnswer_AskForInfoAnswerId",
                table: "AskForInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_AskForInfo_AskForInfoAnswer_AskForInfoAnswerId",
                table: "AskForInfo",
                column: "AskForInfoAnswerId",
                principalTable: "AskForInfoAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AskForInfo_AskForInfoAnswer_AskForInfoAnswerId",
                table: "AskForInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_AskForInfo_AskForInfoAnswer_AskForInfoAnswerId",
                table: "AskForInfo",
                column: "AskForInfoAnswerId",
                principalTable: "AskForInfoAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
