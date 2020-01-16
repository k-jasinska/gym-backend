using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class CarnetTypechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarnetID",
                table: "CarnetTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeCarnetTypeID",
                table: "Carnets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carnets_TypeCarnetTypeID",
                table: "Carnets",
                column: "TypeCarnetTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carnets_CarnetTypes_TypeCarnetTypeID",
                table: "Carnets",
                column: "TypeCarnetTypeID",
                principalTable: "CarnetTypes",
                principalColumn: "CarnetTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carnets_CarnetTypes_TypeCarnetTypeID",
                table: "Carnets");

            migrationBuilder.DropIndex(
                name: "IX_Carnets_TypeCarnetTypeID",
                table: "Carnets");

            migrationBuilder.DropColumn(
                name: "TypeCarnetTypeID",
                table: "Carnets");

            migrationBuilder.AddColumn<Guid>(
                name: "CarnetID",
                table: "CarnetTypes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
