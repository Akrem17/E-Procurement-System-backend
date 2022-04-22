using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_proc.Migrations
{
    public partial class addrepres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepresentativeId",
                table: "Offer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offer_RepresentativeId",
                table: "Offer",
                column: "RepresentativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Representative_RepresentativeId",
                table: "Offer",
                column: "RepresentativeId",
                principalTable: "Representative",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Representative_RepresentativeId",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_RepresentativeId",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "RepresentativeId",
                table: "Offer");
        }
    }
}
