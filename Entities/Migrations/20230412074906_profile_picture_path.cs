using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class profilepicturepath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Teachers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "Students",
                maxLength: 255,
                type: "nvarchar(255)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "Students");
        }
    }
}
