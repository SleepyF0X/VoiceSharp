using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoiceSharp.Persistance.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterAnswer_Answers_AnswerId",
                table: "VoterAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_VoterAnswer_Questions_QuestionId",
                table: "VoterAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_VoterAnswer_VoterQuizzes_VoterQuizId",
                table: "VoterAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoterAnswer",
                table: "VoterAnswer");

            migrationBuilder.RenameTable(
                name: "VoterAnswer",
                newName: "VoterAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_VoterAnswer_VoterQuizId",
                table: "VoterAnswers",
                newName: "IX_VoterAnswers_VoterQuizId");

            migrationBuilder.RenameIndex(
                name: "IX_VoterAnswer_QuestionId",
                table: "VoterAnswers",
                newName: "IX_VoterAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_VoterAnswer_AnswerId",
                table: "VoterAnswers",
                newName: "IX_VoterAnswers_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoterAnswers",
                table: "VoterAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAnswers_Answers_AnswerId",
                table: "VoterAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAnswers_Questions_QuestionId",
                table: "VoterAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAnswers_VoterQuizzes_VoterQuizId",
                table: "VoterAnswers",
                column: "VoterQuizId",
                principalTable: "VoterQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterAnswers_Answers_AnswerId",
                table: "VoterAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoterAnswers_Questions_QuestionId",
                table: "VoterAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoterAnswers_VoterQuizzes_VoterQuizId",
                table: "VoterAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoterAnswers",
                table: "VoterAnswers");

            migrationBuilder.RenameTable(
                name: "VoterAnswers",
                newName: "VoterAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_VoterAnswers_VoterQuizId",
                table: "VoterAnswer",
                newName: "IX_VoterAnswer_VoterQuizId");

            migrationBuilder.RenameIndex(
                name: "IX_VoterAnswers_QuestionId",
                table: "VoterAnswer",
                newName: "IX_VoterAnswer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_VoterAnswers_AnswerId",
                table: "VoterAnswer",
                newName: "IX_VoterAnswer_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoterAnswer",
                table: "VoterAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAnswer_Answers_AnswerId",
                table: "VoterAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAnswer_Questions_QuestionId",
                table: "VoterAnswer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoterAnswer_VoterQuizzes_VoterQuizId",
                table: "VoterAnswer",
                column: "VoterQuizId",
                principalTable: "VoterQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
