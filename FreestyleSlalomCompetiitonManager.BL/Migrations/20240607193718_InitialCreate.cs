using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreestyleSlalomCompetitionManager.BL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BattleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BattleRounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleRounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    WSID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skaters",
                columns: table => new
                {
                    WSID = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    FamilyName = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    AgeCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    SexCategory = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skaters", x => x.WSID);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitions_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorldRank",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    WSID = table.Column<string>(type: "TEXT", nullable: false),
                    Discipline = table.Column<int>(type: "INTEGER", nullable: false),
                    AgeCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    SexCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<ushort>(type: "INTEGER", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SkaterWSID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorldRank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorldRank_Skaters_SkaterWSID",
                        column: x => x.SkaterWSID,
                        principalTable: "Skaters",
                        principalColumn: "WSID");
                });

            migrationBuilder.CreateTable(
                name: "BaseDiscipline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AgeCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    SexCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    DisciplineType = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDiscipline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseDiscipline_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClassicRounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClassicId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassicRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassicRounds_BaseDiscipline_ClassicId",
                        column: x => x.ClassicId,
                        principalTable: "BaseDiscipline",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Competitors",
                columns: table => new
                {
                    WSID = table.Column<string>(type: "TEXT", nullable: false),
                    PayedFee = table.Column<bool>(type: "INTEGER", nullable: false),
                    SendMusic = table.Column<int>(type: "INTEGER", nullable: false),
                    Music = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ShirtSize = table.Column<string>(type: "TEXT", nullable: true),
                    Birthdate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    CompetitionRankBattle = table.Column<int>(type: "INTEGER", nullable: true),
                    CompetitionRankSpeed = table.Column<int>(type: "INTEGER", nullable: true),
                    CompetitionRankClassic = table.Column<int>(type: "INTEGER", nullable: true),
                    CompetitionRankJump = table.Column<int>(type: "INTEGER", nullable: true),
                    CompetitionResultBattle = table.Column<int>(type: "INTEGER", nullable: true),
                    CompetitionResultSpeed = table.Column<int>(type: "INTEGER", nullable: true),
                    CompetitionResultClassic = table.Column<int>(type: "INTEGER", nullable: true),
                    CompetitionResultJump = table.Column<int>(type: "INTEGER", nullable: true),
                    BaseDisciplineId = table.Column<Guid>(type: "TEXT", nullable: true),
                    BattleRoundId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ClassicRoundId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CompetitionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    FamilyName = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    AgeCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    SexCategory = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitors", x => x.WSID);
                    table.ForeignKey(
                        name: "FK_Competitors_BaseDiscipline_BaseDisciplineId",
                        column: x => x.BaseDisciplineId,
                        principalTable: "BaseDiscipline",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Competitors_BattleRounds_BattleRoundId",
                        column: x => x.BattleRoundId,
                        principalTable: "BattleRounds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Competitors_ClassicRounds_ClassicRoundId",
                        column: x => x.ClassicRoundId,
                        principalTable: "ClassicRounds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Competitors_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BattleGroupCompetitor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompetitorWSID = table.Column<string>(type: "TEXT", nullable: true),
                    RankInGroup = table.Column<int>(type: "INTEGER", nullable: false),
                    BattleGroupId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleGroupCompetitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleGroupCompetitor_BattleGroups_BattleGroupId",
                        column: x => x.BattleGroupId,
                        principalTable: "BattleGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BattleGroupCompetitor_Competitors_CompetitorWSID",
                        column: x => x.CompetitorWSID,
                        principalTable: "Competitors",
                        principalColumn: "WSID");
                });

            migrationBuilder.CreateTable(
                name: "ClassicRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompetitorWSID = table.Column<string>(type: "TEXT", nullable: true),
                    FinalMark = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassicRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassicRuns_Competitors_CompetitorWSID",
                        column: x => x.CompetitorWSID,
                        principalTable: "Competitors",
                        principalColumn: "WSID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseDiscipline_CompetitionId",
                table: "BaseDiscipline",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleGroupCompetitor_BattleGroupId",
                table: "BattleGroupCompetitor",
                column: "BattleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleGroupCompetitor_CompetitorWSID",
                table: "BattleGroupCompetitor",
                column: "CompetitorWSID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassicRounds_ClassicId",
                table: "ClassicRounds",
                column: "ClassicId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassicRuns_CompetitorWSID",
                table: "ClassicRuns",
                column: "CompetitorWSID");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_OrganizerId",
                table: "Competitions",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_BaseDisciplineId",
                table: "Competitors",
                column: "BaseDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_BattleRoundId",
                table: "Competitors",
                column: "BattleRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_ClassicRoundId",
                table: "Competitors",
                column: "ClassicRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitors_CompetitionId",
                table: "Competitors",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorldRank_SkaterWSID",
                table: "WorldRank",
                column: "SkaterWSID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleGroupCompetitor");

            migrationBuilder.DropTable(
                name: "ClassicRuns");

            migrationBuilder.DropTable(
                name: "WorldRank");

            migrationBuilder.DropTable(
                name: "BattleGroups");

            migrationBuilder.DropTable(
                name: "Competitors");

            migrationBuilder.DropTable(
                name: "Skaters");

            migrationBuilder.DropTable(
                name: "BattleRounds");

            migrationBuilder.DropTable(
                name: "ClassicRounds");

            migrationBuilder.DropTable(
                name: "BaseDiscipline");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Organizers");
        }
    }
}
