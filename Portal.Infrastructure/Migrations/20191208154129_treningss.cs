using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class treningss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTrainings_People_ClientPersonID",
                table: "PersonTrainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTrainings",
                table: "PersonTrainings");

            migrationBuilder.DropIndex(
                name: "IX_PersonTrainings_ClientPersonID",
                table: "PersonTrainings");

            migrationBuilder.DropColumn(
                name: "ClientPersonID",
                table: "PersonTrainings");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonTrainingID",
                table: "PersonTrainings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTrainings",
                table: "PersonTrainings",
                column: "PersonTrainingID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTrainings_PersonID",
                table: "PersonTrainings",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTrainings_People_PersonID",
                table: "PersonTrainings",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTrainings_People_PersonID",
                table: "PersonTrainings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonTrainings",
                table: "PersonTrainings");

            migrationBuilder.DropIndex(
                name: "IX_PersonTrainings_PersonID",
                table: "PersonTrainings");

            migrationBuilder.DropColumn(
                name: "PersonTrainingID",
                table: "PersonTrainings");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientPersonID",
                table: "PersonTrainings",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonTrainings",
                table: "PersonTrainings",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTrainings_ClientPersonID",
                table: "PersonTrainings",
                column: "ClientPersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTrainings_People_ClientPersonID",
                table: "PersonTrainings",
                column: "ClientPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
