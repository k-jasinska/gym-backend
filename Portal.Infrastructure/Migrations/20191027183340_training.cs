using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class training : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    TrainingID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    ClientPersonID = table.Column<Guid>(nullable: true),
                    TrainerPersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.TrainingID);
                    table.ForeignKey(
                        name: "FK_Training_People_ClientPersonID",
                        column: x => x.ClientPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Training_People_TrainerPersonID",
                        column: x => x.TrainerPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Training_ClientPersonID",
                table: "Training",
                column: "ClientPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Training_TrainerPersonID",
                table: "Training",
                column: "TrainerPersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Training");
        }
    }
}
