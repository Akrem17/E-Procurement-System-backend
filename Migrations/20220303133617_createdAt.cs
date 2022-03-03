using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_proc.Migrations
{
    public partial class createdAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    street1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    street2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcquisitionDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuingInstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Representative",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialSecurityNumberDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representative", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citizen_Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameFr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfInstitute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    representativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institute_addressId = table.Column<int>(type: "int", nullable: true),
                    Institute_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institute_Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterlocutorId = table.Column<int>(type: "int", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNSSId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuisnessClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuisnessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    representativeId = table.Column<int>(type: "int", nullable: true),
                    licenceId = table.Column<int>(type: "int", nullable: true),
                    addressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Address_addressId",
                        column: x => x.addressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Address_Institute_addressId",
                        column: x => x.Institute_addressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Licence_licenceId",
                        column: x => x.licenceId,
                        principalTable: "Licence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Representative_InterlocutorId",
                        column: x => x.InterlocutorId,
                        principalTable: "Representative",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Representative_representativeId",
                        column: x => x.representativeId,
                        principalTable: "Representative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_addressId",
                table: "Users",
                column: "addressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Institute_addressId",
                table: "Users",
                column: "Institute_addressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InterlocutorId",
                table: "Users",
                column: "InterlocutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_licenceId",
                table: "Users",
                column: "licenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_representativeId",
                table: "Users",
                column: "representativeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UsersLogin");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Licence");

            migrationBuilder.DropTable(
                name: "Representative");
        }
    }
}
