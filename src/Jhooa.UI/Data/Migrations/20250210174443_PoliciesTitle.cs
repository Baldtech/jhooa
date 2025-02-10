using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhooa.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class PoliciesTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleFr",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "TitleFr",
                table: "Policies");
        }
    }
}
