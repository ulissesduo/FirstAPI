using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicEntities");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "MusicEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusicEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AlbumTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Song_MusicEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicEntities_MusicEntities_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "MusicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MusicEntities_MusicEntities_MusicEntityId",
                        column: x => x.MusicEntityId,
                        principalTable: "MusicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MusicEntities_MusicEntities_Song_MusicEntityId",
                        column: x => x.Song_MusicEntityId,
                        principalTable: "MusicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicEntities_AlbumId",
                table: "MusicEntities",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicEntities_MusicEntityId",
                table: "MusicEntities",
                column: "MusicEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicEntities_Song_MusicEntityId",
                table: "MusicEntities",
                column: "Song_MusicEntityId");
        }
    }
}
