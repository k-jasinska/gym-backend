using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Infrastructure.Migrations
{
    public partial class removeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Training_GroupTrainingTrainingID",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_People_ParticipantPersonID",
                table: "Training");

            migrationBuilder.DropIndex(
                name: "IX_Training_ParticipantPersonID",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "ParticipantPersonID",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Training");

            migrationBuilder.RenameColumn(
                name: "GroupTrainingTrainingID",
                table: "People",
                newName: "TrainingID");

            migrationBuilder.RenameIndex(
                name: "IX_People_GroupTrainingTrainingID",
                table: "People",
                newName: "IX_People_TrainingID");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Training",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Training_TrainingID",
                table: "People",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "TrainingID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Training_TrainingID",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "TrainingID",
                table: "People",
                newName: "GroupTrainingTrainingID");

            migrationBuilder.RenameIndex(
                name: "IX_People_TrainingID",
                table: "People",
                newName: "IX_People_GroupTrainingTrainingID");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Training",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Guid>(
                name: "ParticipantPersonID",
                table: "Training",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Training",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Training_ParticipantPersonID",
                table: "Training",
                column: "ParticipantPersonID");

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
    }
}
