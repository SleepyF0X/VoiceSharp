using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VoiceSharp.Persistance.Migrations
{
    public partial class AddVoterAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "VoterQuizzes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VoterQuizzes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoterToken",
                table: "VoterQuizzes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "VoterAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoterQuizId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    AnswerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoterAnswer_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoterAnswer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoterAnswer_VoterQuizzes_VoterQuizId",
                        column: x => x.VoterQuizId,
                        principalTable: "VoterQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoterAnswer_AnswerId",
                table: "VoterAnswer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_VoterAnswer_QuestionId",
                table: "VoterAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_VoterAnswer_VoterQuizId",
                table: "VoterAnswer",
                column: "VoterQuizId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoterAnswer");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VoterQuizzes");

            migrationBuilder.DropColumn(
                name: "VoterToken",
                table: "VoterQuizzes");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpirationDate",
                table: "VoterQuizzes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
