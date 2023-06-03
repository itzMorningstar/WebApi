using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class accountsTableupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Accounts",
               columns: table => new
               {
                   AccountGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   Username = table.Column<string>(type: "nvarchar(200)", nullable: false),
                   Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Salt = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   Phone = table.Column<string>(type: "nvarchar(50)", nullable: false),
                   IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                   IsPhoneVerified = table.Column<bool>(type: "bit", nullable: false),
                   IsLocked = table.Column<bool>(type: "bit", nullable: false),
                   IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                   CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                   UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                   UpdatedOn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   LastLogin = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   LastLoginIP = table.Column<string>(type: "nvarchar(200)", nullable: false),
                   LastLoginUserAgent = table.Column<string>(type: "nvarchar(200)", nullable: false),
                   LastLoginLocation = table.Column<string>(type: "nvarchar(200)", nullable: false),
                   LastLoginDevice = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   LastLoginOS = table.Column<string>(type: "nvarchar(100)", nullable: false),
                   LastLoginBrowser = table.Column<string>(type: "nvarchar(50)", nullable: false),
                   ApiKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   ApiKeyUpdatedOn = table.Column<string>(type: "nvarchar(100)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Accounts", x => x.AccountGuid);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
