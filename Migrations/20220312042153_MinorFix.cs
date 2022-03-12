using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroApi.Migrations
{
    public partial class MinorFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Addresses",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Addresses");
        }
    }
}
