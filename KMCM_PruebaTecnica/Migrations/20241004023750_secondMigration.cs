using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KMCM_PruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID_USER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PASSWORD = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    kmcm_person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__ID_USER", x => x.ID_USER);
                    table.ForeignKey(
                        name: "FK_User_Person",
                        column: x => x.kmcm_person_id,
                        principalTable: "Persons",
                        principalColumn: "ID_PERSON",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_kmcm_person_id",
                table: "Users",
                column: "kmcm_person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
