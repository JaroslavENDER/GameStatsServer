using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameStatsServer.Migrations
{
    public partial class AddServersAndMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Endpoint = table.Column<string>(nullable: false),
                    GameModes = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Endpoint);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    FragLimit = table.Column<int>(nullable: false),
                    GameMode = table.Column<string>(nullable: true),
                    Map = table.Column<string>(nullable: true),
                    ServerEndpoint = table.Column<string>(nullable: true),
                    TimeElapsed = table.Column<double>(nullable: false),
                    TimeLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Timestamp);
                    table.ForeignKey(
                        name: "FK_Matches_Servers_ServerEndpoint",
                        column: x => x.ServerEndpoint,
                        principalTable: "Servers",
                        principalColumn: "Endpoint",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deaths = table.Column<int>(nullable: false),
                    Frags = table.Column<int>(nullable: false),
                    Kills = table.Column<int>(nullable: false),
                    MatchTimestamp = table.Column<DateTime>(nullable: true),
                    PlayerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Matches_MatchTimestamp",
                        column: x => x.MatchTimestamp,
                        principalTable: "Matches",
                        principalColumn: "Timestamp",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ServerEndpoint",
                table: "Matches",
                column: "ServerEndpoint");

            migrationBuilder.CreateIndex(
                name: "IX_Score_MatchTimestamp",
                table: "Score",
                column: "MatchTimestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
