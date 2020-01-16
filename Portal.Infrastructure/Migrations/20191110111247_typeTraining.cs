using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class typeTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_People_ClientPersonID",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Training");

            migrationBuilder.RenameColumn(
                name: "ClientPersonID",
                table: "Training",
                newName: "ParticipantPersonID");

            migrationBuilder.RenameIndex(
                name: "IX_Training_ClientPersonID",
                table: "Training",
                newName: "IX_Training_ParticipantPersonID");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Training",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Training",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Training",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Training",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupTrainingTrainingID",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_GroupTrainingTrainingID",
                table: "People",
                column: "GroupTrainingTrainingID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Training_GroupTrainingTrainingID",
                table: "People",
                column: "GroupTrainingTrainingID",
                principalTable: "Training",
                principalColumn: "TrainingID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_People_ParticipantPersonID",
                table: "Training",
                column: "ParticipantPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Training_GroupTrainingTrainingID",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_People_ParticipantPersonID",
                table: "Training");

            migrationBuilder.DropIndex(
                name: "IX_People_GroupTrainingTrainingID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "GroupTrainingTrainingID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "ParticipantPersonID",
                table: "Training",
                newName: "ClientPersonID");

            migrationBuilder.RenameIndex(
                name: "IX_Training_ParticipantPersonID",
                table: "Training",
                newName: "IX_Training_ClientPersonID");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Training",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Training_People_ClientPersonID",
                table: "Training",
                column: "ClientPersonID",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
