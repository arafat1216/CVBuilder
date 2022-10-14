using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.Infrastructure.Migrations
{
    public partial class AddedSubjectinDegreeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Degrees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Degrees");
        }
    }
}
