using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autotest_MVC_2.Migrations
{
    public partial class firstMg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentTicketIndex = table.Column<int>(type: "int", nullable: true),
                    IsCompletedTicketCount = table.Column<int>(type: "int", nullable: false),
                    TotalCorrectAnswerCount = table.Column<int>(type: "int", nullable: false),
                    IsExam = table.Column<bool>(type: "bit", nullable: false),
                    ExamCurrentTicketIndex = table.Column<int>(type: "int", nullable: false),
                    CurrentTicketTicketId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmailOrPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketIndex = table.Column<int>(type: "int", nullable: false),
                    QuestionsCount = table.Column<int>(type: "int", nullable: false),
                    StarQuestionIndex = table.Column<int>(type: "int", nullable: false),
                    CurrentQuestionIndex = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_AllUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TicketQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionIndex = table.Column<int>(type: "int", nullable: false),
                    ChoiceIndex = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswerIndex = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketQuestionAnswers_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllUsers_CurrentTicketTicketId",
                table: "AllUsers",
                column: "CurrentTicketTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketQuestionAnswers_TicketId",
                table: "TicketQuestionAnswers",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllUsers_Tickets_CurrentTicketTicketId",
                table: "AllUsers",
                column: "CurrentTicketTicketId",
                principalTable: "Tickets",
                principalColumn: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllUsers_Tickets_CurrentTicketTicketId",
                table: "AllUsers");

            migrationBuilder.DropTable(
                name: "TicketQuestionAnswers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AllUsers");
        }
    }
}
