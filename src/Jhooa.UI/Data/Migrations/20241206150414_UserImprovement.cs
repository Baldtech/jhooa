using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhooa.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserImprovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptTos",
                table: "IdentityUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "IdentityUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "IdentityUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "IdentityUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NewsletterActive",
                table: "IdentityUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "IdentityUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptTos",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "NewsletterActive",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "IdentityUsers");
        }
    }
}
