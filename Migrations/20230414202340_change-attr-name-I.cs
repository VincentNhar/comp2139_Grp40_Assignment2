using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grp40_Assignment2.Migrations
{
    public partial class changeattrnameI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sellerId",
                schema: "Identity",
                table: "UserReview",
                newName: "seller");

            migrationBuilder.RenameColumn(
                name: "reviewerId",
                schema: "Identity",
                table: "UserReview",
                newName: "reviewer");

            migrationBuilder.RenameColumn(
                name: "buyer_feedback",
                schema: "Identity",
                table: "UserReview",
                newName: "feedback");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "seller",
                schema: "Identity",
                table: "UserReview",
                newName: "sellerId");

            migrationBuilder.RenameColumn(
                name: "reviewer",
                schema: "Identity",
                table: "UserReview",
                newName: "reviewerId");

            migrationBuilder.RenameColumn(
                name: "feedback",
                schema: "Identity",
                table: "UserReview",
                newName: "buyer_feedback");
        }
    }
}
