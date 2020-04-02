using Microsoft.EntityFrameworkCore.Migrations;

namespace PodcastApp.Data.Migrations
{
    public partial class cambiosnuevos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainReview",
                table: "PodcastMovie",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainReview",
                table: "PodcastMovie");
        }
    }
}
