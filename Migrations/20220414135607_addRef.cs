using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_proc.Migrations
{
    public partial class addRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "Notification");

            migrationBuilder.AddColumn<int>(
                name: "InstituteId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_InstituteId",
                table: "Notification",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OfferId",
                table: "Notification",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Offer_OfferId",
                table: "Notification",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_InstituteId",
                table: "Notification",
                column: "InstituteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Offer_OfferId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_InstituteId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_InstituteId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_OfferId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "InstituteId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "FromId",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToUserId",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
