using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class trenings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Training_TrainingID",
                table: "People");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "SharedMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_People_TrainingID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "TrainingID",
                table: "People");

            migrationBuilder.CreateTable(
                name: "PersonTrainings",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(nullable: false),
                    ClientPersonID = table.Column<Guid>(nullable: true),
                    TrainigID = table.Column<Guid>(nullable: false),
                    TrainingID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTrainings", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_PersonTrainings_People_ClientPersonID",
                        column: x => x.ClientPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonTrainings_Training_TrainingID",
                        column: x => x.TrainingID,
                        principalTable: "Training",
                        principalColumn: "TrainingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonTrainings_ClientPersonID",
                table: "PersonTrainings",
                column: "ClientPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTrainings_TrainingID",
                table: "PersonTrainings",
                column: "TrainingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonTrainings");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingID",
                table: "People",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    PressureDiastolic = table.Column<int>(nullable: false),
                    PressureSystolic = table.Column<int>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementID);
                    table.ForeignKey(
                        name: "FK_Measurements_People_ClientID",
                        column: x => x.ClientID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedMeasurements",
                columns: table => new
                {
                    SharedID = table.Column<Guid>(nullable: false),
                    ClientPersonID = table.Column<Guid>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TrainerPersonID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedMeasurements", x => x.SharedID);
                    table.ForeignKey(
                        name: "FK_SharedMeasurements_People_ClientPersonID",
                        column: x => x.ClientPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SharedMeasurements_People_TrainerPersonID",
                        column: x => x.TrainerPersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_TrainingID",
                table: "People",
                column: "TrainingID");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_ClientID",
                table: "Measurements",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_SharedMeasurements_ClientPersonID",
                table: "SharedMeasurements",
                column: "ClientPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_SharedMeasurements_TrainerPersonID",
                table: "SharedMeasurements",
                column: "TrainerPersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Training_TrainingID",
                table: "People",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "TrainingID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
