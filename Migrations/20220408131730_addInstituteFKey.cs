using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_proc.Migrations
{
    public partial class addInstituteFKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_Institute_addressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Representative_InterlocutorId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "InterlocutorId",
                table: "Users",
                newName: "interlocutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_InterlocutorId",
                table: "Users",
                newName: "IX_Users_interlocutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_Institute_addressId",
                table: "Users",
                column: "Institute_addressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Representative_interlocutorId",
                table: "Users",
                column: "interlocutorId",
                principalTable: "Representative",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_Institute_addressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Representative_interlocutorId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "interlocutorId",
                table: "Users",
                newName: "InterlocutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_interlocutorId",
                table: "Users",
                newName: "IX_Users_InterlocutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_Institute_addressId",
                table: "Users",
                column: "Institute_addressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Representative_InterlocutorId",
                table: "Users",
                column: "InterlocutorId",
                principalTable: "Representative",
                principalColumn: "Id");
        }
    }
}
