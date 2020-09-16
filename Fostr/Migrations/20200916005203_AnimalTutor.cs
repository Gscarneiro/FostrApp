using Microsoft.EntityFrameworkCore.Migrations;

namespace Fostr.Migrations
{
    public partial class AnimalTutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalTutor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FostrUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalTutor_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalTutor_AspNetUsers_FostrUserId",
                        column: x => x.FostrUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalTutor_AnimalId",
                table: "AnimalTutor",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalTutor_FostrUserId",
                table: "AnimalTutor",
                column: "FostrUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalTutor");
        }
    }
}
