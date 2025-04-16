using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhooa.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class VimeoRevamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "Videos",
                newName: "SubscribedVimeoId");

            migrationBuilder.AddColumn<string>(
                name: "NotSubscribedVimeoHash",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NotSubscribedVimeoId",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubscribedVimeoHash",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotSubscribedVimeoHash",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "NotSubscribedVimeoId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "SubscribedVimeoHash",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "SubscribedVimeoId",
                table: "Videos",
                newName: "VideoUrl");
        }
    }
}
