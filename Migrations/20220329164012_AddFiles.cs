using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_proc.Migrations
{
    public partial class AddFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMontant = table.Column<int>(type: "int", nullable: false),
                    FinalTotalMontant = table.Column<int>(type: "int", nullable: true),
                    isAccepted = table.Column<bool>(type: "bit", nullable: true),
                    TenderId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_Tender_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offer_Users_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

           
            migrationBuilder.CreateTable(
                name: "FileData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenderId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileData_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FileData_Tender_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tender",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileData_OfferId",
                table: "FileData",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_FileData_TenderId",
                table: "FileData",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_SupplierId",
                table: "Offer",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_TenderId",
                table: "Offer",
                column: "TenderId");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileData");

      

            migrationBuilder.DropTable(
                name: "Offer");

        }
    }
}
