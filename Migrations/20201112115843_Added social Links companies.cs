using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBuilder.Migrations
{
    public partial class AddedsocialLinkscompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FbProfile",
                table: "Companies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "linkedinProfile",
                table: "Companies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "twitterProfile",
                table: "Companies",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FbProfile",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "linkedinProfile",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "twitterProfile",
                table: "Companies");
        }
    }
}
