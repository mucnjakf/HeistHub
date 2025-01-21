using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeistHub.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeistSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MembersRequired = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeistSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSkills", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "HeistMember",
                columns: table => new
                {
                    HeistsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeistMember", x => new { x.HeistsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_HeistMember_Heists_HeistsId",
                        column: x => x.HeistsId,
                        principalTable: "Heists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeistMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_HeistHeistSkill_RequiredSkillsId",
                table: "HeistHeistSkill",
                column: "RequiredSkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_HeistMember_MembersId",
                table: "HeistMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Heists_Name",
                table: "Heists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeistSkills_Name",
                table: "HeistSkills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberMemberSkill_SkillsId",
                table: "MemberMemberSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSkills_Name",
                table: "MemberSkills",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeistHeistSkill");

            migrationBuilder.DropTable(
                name: "HeistMember");

            migrationBuilder.DropTable(
                name: "MemberMemberSkill");

            migrationBuilder.DropTable(
                name: "HeistSkills");

            migrationBuilder.DropTable(
                name: "Heists");

            migrationBuilder.DropTable(
                name: "MemberSkills");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
