using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grp40_Assignment2.Migrations
{
    public partial class updateuserreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "buyerId",
                schema: "Identity",
                table: "UserReview",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "buyer_feedback",
                schema: "Identity",
                table: "UserReview",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "buyer_rating",
                schema: "Identity",
                table: "UserReview",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sellerId",
                schema: "Identity",
                table: "UserReview",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "buyerId",
                schema: "Identity",
                table: "UserReview");

            migrationBuilder.DropColumn(
                name: "buyer_feedback",
                schema: "Identity",
                table: "UserReview");

            migrationBuilder.DropColumn(
                name: "buyer_rating",
                schema: "Identity",
                table: "UserReview");

            migrationBuilder.DropColumn(
                name: "sellerId",
                schema: "Identity",
                table: "UserReview");
        }
    }
}
