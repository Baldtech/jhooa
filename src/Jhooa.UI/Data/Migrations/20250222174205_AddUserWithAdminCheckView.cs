using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhooa.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserWithAdminCheckView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW UserWithAdminCheck AS
                SELECT 
                    u.Id,
                    u.FirstName, 
                    u.LastName, 
                    u.Email,
                    CASE WHEN r.Name = 'Admin' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsAdmin,
                    CASE WHEN s.UserId IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsSubscriptionActive,
                    s.[Type] AS SubscriptionType,
                    s.[Status] AS SubscriptionStatus,
                    s.[Start] AS SubscriptionStartDate,
                    s.[End] AS SubscriptionEndDate,
                    s.[Id] AS SubscriptionId,
                    s.[StripeSubscriptionId] AS StripeSubscriptionId
                FROM [dbo].[IdentityUsers] u
                LEFT JOIN [dbo].[IdentityUserRoles] ur ON u.Id = ur.UserId
                LEFT JOIN [dbo].[IdentityRoles] r ON ur.RoleId = r.Id
                LEFT JOIN [dbo].[Subscriptions] s ON u.Id = s.UserId AND s.[Start] <= GETDATE() AND (s.[End] IS NULL OR s.[End] >= GETDATE())
                WHERE u.ActivatedAt IS NOT NULL;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS UserWithAdminCheck;");

        }
    }
}
