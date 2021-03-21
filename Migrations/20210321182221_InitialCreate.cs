using Microsoft.EntityFrameworkCore.Migrations;

namespace Taxonomix.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datasets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Scientific = table.Column<string>(type: "TEXT", nullable: true),
                    Vernacular = table.Column<string>(type: "TEXT", nullable: true),
                    Chinese = table.Column<string>(type: "TEXT", nullable: true),
                    English = table.Column<string>(type: "TEXT", nullable: true),
                    French = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemName", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    NameId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_ItemName_NameId",
                        column: x => x.NameId,
                        principalTable: "ItemName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Taxon",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    NameId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxon_ItemName_NameId",
                        column: x => x.NameId,
                        principalTable: "ItemName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hierarchy<Character>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntryId = table.Column<string>(type: "TEXT", nullable: true),
                    DatasetId = table.Column<string>(type: "TEXT", nullable: true),
                    HierarchyCharacterId = table.Column<int>(name: "Hierarchy<Character>Id", type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hierarchy<Character>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hierarchy<Character>_Character_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hierarchy<Character>_Datasets_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Datasets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hierarchy<Character>_Hierarchy<Character>_Hierarchy<Character>Id",
                        column: x => x.HierarchyCharacterId,
                        principalTable: "Hierarchy<Character>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookReference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fasc = table.Column<string>(type: "TEXT", nullable: true),
                    Page = table.Column<int>(type: "INTEGER", nullable: false),
                    Detail = table.Column<string>(type: "TEXT", nullable: true),
                    TaxonId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReference_Taxon_TaxonId",
                        column: x => x.TaxonId,
                        principalTable: "Taxon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hierarchy<Taxon>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntryId = table.Column<string>(type: "TEXT", nullable: true),
                    DatasetId = table.Column<string>(type: "TEXT", nullable: true),
                    HierarchyTaxonId = table.Column<int>(name: "Hierarchy<Taxon>Id", type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hierarchy<Taxon>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hierarchy<Taxon>_Datasets_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Datasets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hierarchy<Taxon>_Hierarchy<Taxon>_Hierarchy<Taxon>Id",
                        column: x => x.HierarchyTaxonId,
                        principalTable: "Hierarchy<Taxon>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hierarchy<Taxon>_Taxon_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Taxon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Legend = table.Column<string>(type: "TEXT", nullable: true),
                    Source = table.Column<string>(type: "TEXT", nullable: true),
                    CharacterId = table.Column<string>(type: "TEXT", nullable: true),
                    TaxonId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Picture_Taxon_TaxonId",
                        column: x => x.TaxonId,
                        principalTable: "Taxon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookReference_TaxonId",
                table: "BookReference",
                column: "TaxonId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_NameId",
                table: "Character",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Hierarchy<Character>_DatasetId",
                table: "Hierarchy<Character>",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_Hierarchy<Character>_EntryId",
                table: "Hierarchy<Character>",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hierarchy<Character>_Hierarchy<Character>Id",
                table: "Hierarchy<Character>",
                column: "Hierarchy<Character>Id");

            migrationBuilder.CreateIndex(
                name: "IX_Hierarchy<Taxon>_DatasetId",
                table: "Hierarchy<Taxon>",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_Hierarchy<Taxon>_EntryId",
                table: "Hierarchy<Taxon>",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Hierarchy<Taxon>_Hierarchy<Taxon>Id",
                table: "Hierarchy<Taxon>",
                column: "Hierarchy<Taxon>Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_CharacterId",
                table: "Picture",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_TaxonId",
                table: "Picture",
                column: "TaxonId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxon_NameId",
                table: "Taxon",
                column: "NameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookReference");

            migrationBuilder.DropTable(
                name: "Hierarchy<Character>");

            migrationBuilder.DropTable(
                name: "Hierarchy<Taxon>");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "Datasets");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Taxon");

            migrationBuilder.DropTable(
                name: "ItemName");
        }
    }
}
