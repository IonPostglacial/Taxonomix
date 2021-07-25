using Microsoft.EntityFrameworkCore.Migrations;

namespace Taxonomix.Migrations
{
    public partial class CompleteScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StateId",
                table: "Picture",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<string>(type: "TEXT", nullable: true),
                    CharacterId1 = table.Column<string>(type: "TEXT", nullable: true),
                    TaxonId = table.Column<string>(type: "TEXT", nullable: true),
                    NameId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_State_Character_CharacterId1",
                        column: x => x.CharacterId1,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_State_ItemName_NameId",
                        column: x => x.NameId,
                        principalTable: "ItemName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_State_Taxon_TaxonId",
                        column: x => x.TaxonId,
                        principalTable: "Taxon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_StateId",
                table: "Picture",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CharacterId",
                table: "State",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CharacterId1",
                table: "State",
                column: "CharacterId1");

            migrationBuilder.CreateIndex(
                name: "IX_State_NameId",
                table: "State",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_State_TaxonId",
                table: "State",
                column: "TaxonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_State_StateId",
                table: "Picture",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_State_StateId",
                table: "Picture");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropIndex(
                name: "IX_Picture_StateId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Picture");
        }
    }
}
