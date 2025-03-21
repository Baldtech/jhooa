using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhooa.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Companies2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCode_Company_CompanyId",
                table: "CompanyCode");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCode_Subscriptions_SubscriptionId",
                table: "CompanyCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCode",
                table: "CompanyCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "CompanyCode",
                newName: "CompanyCodes");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCode_SubscriptionId",
                table: "CompanyCodes",
                newName: "IX_CompanyCodes_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCode_CompanyId",
                table: "CompanyCodes",
                newName: "IX_CompanyCodes_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCodes",
                table: "CompanyCodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCodes_Companies_CompanyId",
                table: "CompanyCodes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCodes_Subscriptions_SubscriptionId",
                table: "CompanyCodes",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCodes_Companies_CompanyId",
                table: "CompanyCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCodes_Subscriptions_SubscriptionId",
                table: "CompanyCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCodes",
                table: "CompanyCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "CompanyCodes",
                newName: "CompanyCode");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCodes_SubscriptionId",
                table: "CompanyCode",
                newName: "IX_CompanyCode_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCodes_CompanyId",
                table: "CompanyCode",
                newName: "IX_CompanyCode_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCode",
                table: "CompanyCode",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCode_Company_CompanyId",
                table: "CompanyCode",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCode_Subscriptions_SubscriptionId",
                table: "CompanyCode",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }
    }
}
