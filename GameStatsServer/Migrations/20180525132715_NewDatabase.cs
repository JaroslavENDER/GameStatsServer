using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GameStatsServer.Migrations
{
    public partial class NewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Endpoint = table.Column<string>(nullable: false),
                    GameModes = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Endpoint);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FragLimit = table.Column<int>(nullable: false),
                    GameMode = table.Column<string>(nullable: false),
                    Map = table.Column<string>(nullable: false),
                    ServerEndpoint = table.Column<string>(nullable: false),
                    TimeElapsed = table.Column<double>(nullable: false),
                    TimeLimit = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Servers_ServerEndpoint",
                        column: x => x.ServerEndpoint,
                        principalTable: "Servers",
                        principalColumn: "Endpoint",
                        onDelete: ReferentialAction.Cascade);
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
                    MatchId = table.Column<int>(nullable: false),
                    PlayerName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ServerEndpoint",
                table: "Matches",
                column: "ServerEndpoint");

            migrationBuilder.CreateIndex(
                name: "IX_Score_MatchId",
                table: "Score",
                column: "MatchId");
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
