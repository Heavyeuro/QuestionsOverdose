using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionOverdose.DAL.Migrations
{
    public partial class VotedFunctionalityExt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpvote",
                table: "UserQuestions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpvote",
                table: "UserAnswers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpvote",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "IsUpvote",
                table: "UserAnswers");
        }
    }
}
