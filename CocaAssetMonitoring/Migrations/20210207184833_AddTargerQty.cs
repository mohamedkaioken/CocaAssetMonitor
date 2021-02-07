using Microsoft.EntityFrameworkCore.Migrations;

namespace CocaAssetMonitoring.Migrations
{
    public partial class AddTargerQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetQty",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetQty",
                table: "Settings");
        }
    }
}
