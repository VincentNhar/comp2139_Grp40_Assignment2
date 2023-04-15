using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grp40_Assignment2.Migrations
{
    public partial class changeattrnameII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "buyer_rating",
                schema: "Identity",
                table: "UserReview",
                newName: "rating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rating",
                schema: "Identity",
                table: "UserReview",
                newName: "buyer_rating");
        }
    }
}
