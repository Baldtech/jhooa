using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhooa.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Videos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoVideoTheme",
                columns: table => new
                {
                    ThemesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoVideoTheme", x => new { x.ThemesId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_VideoVideoTheme_VideoThemes_ThemesId",
                        column: x => x.ThemesId,
                        principalTable: "VideoThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoVideoTheme_Videos_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoVideoTheme_VideosId",
                table: "VideoVideoTheme",
                column: "VideosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoVideoTheme");

            migrationBuilder.DropTable(
                name: "Videos");
        }
    }
}
