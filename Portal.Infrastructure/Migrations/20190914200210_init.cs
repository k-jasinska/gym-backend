using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarnetTypes",
                columns: table => new
                {
                    CarnetTypeID = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    CarnetID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarnetTypes", x => x.CarnetTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Entrances",
                columns: table => new
                {
                    EntranceID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    CarnetID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrances", x => x.EntranceID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false),
                    ContactData = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Carnets",
                columns: table => new
                {
                    CarnetID = table.Column<Guid>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carnets", x => x.CarnetID);
                    table.ForeignKey(
                        name: "FK_Carnets_People_ClientID",
                        column: x => x.ClientID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementID = table.Column<Guid>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    PressureSystolic = table.Column<int>(nullable: false),
                    PressureDiastolic = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false)
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
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ClientPersonID = table.Column<Guid>(nullable: true),
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
                name: "IX_Carnets_ClientID",
                table: "Carnets",
                column: "ClientID");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carnets");

            migrationBuilder.DropTable(
                name: "CarnetTypes");

            migrationBuilder.DropTable(
                name: "Entrances");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "SharedMeasurements");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
