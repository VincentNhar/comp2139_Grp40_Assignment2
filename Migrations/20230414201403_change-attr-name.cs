using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grp40_Assignment2.Migrations
{
    public partial class changeattrname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "buyerId",
                schema: "Identity",
                table: "UserReview",
                newName: "reviewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reviewerId",
                schema: "Identity",
                table: "UserReview",
                newName: "buyerId");
        }
    }
}
