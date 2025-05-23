﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhooa.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class DurationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "CompanyCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "CompanyCodes");
        }
    }
}
