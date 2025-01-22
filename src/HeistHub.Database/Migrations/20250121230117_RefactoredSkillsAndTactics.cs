using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeistHub.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredSkillsAndTactics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeistHeistSkill");

            migrationBuilder.DropTable(
                name: "MemberMemberSkill");

            migrationBuilder.DropTable(
                name: "HeistSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSkills",
                table: "MemberSkills");

            migrationBuilder.DropIndex(
                name: "IX_MemberSkills_Name",
                table: "MemberSkills");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "MemberSkills");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MemberSkills");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MemberSkills",
                newName: "SkillId");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "MemberSkills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSkills",
                table: "MemberSkills",
                columns: new[] { "MemberId", "SkillId" });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tactics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MembersRequired = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tactics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeistTactics",
                columns: table => new
                {
                    HeistId = table.Column<Guid>(type: "uuid", nullable: false),
                    TacticId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeistTactics", x => new { x.HeistId, x.TacticId });
                    table.ForeignKey(
                        name: "FK_HeistTactics_Heists_HeistId",
                        column: x => x.HeistId,
                        principalTable: "Heists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeistTactics_Tactics_TacticId",
                        column: x => x.TacticId,
                        principalTable: "Tactics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberSkills_SkillId",
                table: "MemberSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_HeistTactics_TacticId",
                table: "HeistTactics",
                column: "TacticId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkills_Members_MemberId",
                table: "MemberSkills",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkills_Skills_SkillId",
                table: "MemberSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkills_Members_MemberId",
                table: "MemberSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkills_Skills_SkillId",
                table: "MemberSkills");

            migrationBuilder.DropTable(
                name: "HeistTactics");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Tactics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSkills",
                table: "MemberSkills");

            migrationBuilder.DropIndex(
                name: "IX_MemberSkills_SkillId",
                table: "MemberSkills");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "MemberSkills");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "MemberSkills",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "MemberSkills",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MemberSkills",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSkills",
                table: "MemberSkills",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HeistSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MembersRequired = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeistSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberMemberSkill",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberMemberSkill", x => new { x.MemberId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_MemberMemberSkill_MemberSkills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "MemberSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberMemberSkill_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeistHeistSkill",
                columns: table => new
                {
                    HeistId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredSkillsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeistHeistSkill", x => new { x.HeistId, x.RequiredSkillsId });
                    table.ForeignKey(
                        name: "FK_HeistHeistSkill_HeistSkills_RequiredSkillsId",
                        column: x => x.RequiredSkillsId,
                        principalTable: "HeistSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeistHeistSkill_Heists_HeistId",
                        column: x => x.HeistId,
                        principalTable: "Heists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberSkills_Name",
                table: "MemberSkills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeistHeistSkill_RequiredSkillsId",
                table: "HeistHeistSkill",
                column: "RequiredSkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_HeistSkills_Name",
                table: "HeistSkills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberMemberSkill_SkillsId",
                table: "MemberMemberSkill",
                column: "SkillsId");
        }
    }
}
