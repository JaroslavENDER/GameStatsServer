using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameStatsServer.Migrations
{
    public partial class AddedScoresInContextAndLeaderboardPlaceInScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Score",
                table: "Score");

            migrationBuilder.RenameTable(
                name: "Score",
                newName: "Scores");

            migrationBuilder.RenameIndex(
                name: "IX_Score_MatchId",
                table: "Scores",
                newName: "IX_Scores_MatchId");

            migrationBuilder.AddColumn<int>(
                name: "LeaderboardPlace",
                table: "Scores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Matches_MatchId",
                table: "Scores",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Matches_MatchId",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "LeaderboardPlace",
                table: "Scores");

            migrationBuilder.RenameTable(
                name: "Scores",
                newName: "Score");

            migrationBuilder.RenameIndex(
                name: "IX_Scores_MatchId",
                table: "Score",
                newName: "IX_Score_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Score",
                table: "Score",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
