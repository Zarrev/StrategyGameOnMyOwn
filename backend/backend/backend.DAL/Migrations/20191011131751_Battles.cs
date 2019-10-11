using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.DAL.Migrations
{
    public partial class Battles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AssaultSeaDog = table.Column<int>(nullable: false),
                    BattleSeahorse = table.Column<int>(nullable: false),
                    LaserShark = table.Column<int>(nullable: false),
                    EnemyId = table.Column<string>(nullable: true),
                    EnemyAssaultSeaDog = table.Column<int>(nullable: false),
                    EnemyBattleSeahorse = table.Column<int>(nullable: false),
                    EnemyLaserShark = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_AspNetUsers_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_EnemyId",
                table: "Battles",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_UserId",
                table: "Battles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");
        }
    }
}
