using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class profileComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ProfileComplete",
                table: "People",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ProfileComplete",
                table: "People",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
