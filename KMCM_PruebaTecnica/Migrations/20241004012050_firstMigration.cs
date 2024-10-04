using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KMCM_PruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID_PERSON = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LASTNAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ADDRESS = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    PHONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    BIRTHDATE = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Persons__ID_PERSON", x => x.ID_PERSON);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
