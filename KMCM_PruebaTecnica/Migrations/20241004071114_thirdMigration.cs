using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KMCM_PruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class thirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KMCM_Persons",
                columns: table => new
                {
                    KMCM_ID_PERSON = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KMCM_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    KMCM_LASTNAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    KMCM_ADDRESS = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    KMCM_PHONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    KMCM_BIRTHDATE = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Persons__ID_PERSON", x => x.KMCM_ID_PERSON);
                });

            migrationBuilder.CreateTable(
                name: "KMCM_Users",
                columns: table => new
                {
                    KMCM_ID_USER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KMCM_USERNAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    KMCM_PASSWORD = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    kmcm_person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__ID_USER", x => x.KMCM_ID_USER);
                    table.ForeignKey(
                        name: "FK_User_Person",
                        column: x => x.kmcm_person_id,
                        principalTable: "KMCM_Persons",
                        principalColumn: "KMCM_ID_PERSON",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KMCM_Users_kmcm_person_id",
                table: "KMCM_Users",
                column: "kmcm_person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KMCM_Users");

            migrationBuilder.DropTable(
                name: "KMCM_Persons");
        }
    }
}
