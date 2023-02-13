using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ItemMigration.Migrations
{
    public partial class develop20230211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElementEnhanceStoneDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElementAttackEnhance = table.Column<int>(nullable: false),
                    ElementDefenceEnhance = table.Column<int>(nullable: false),
                    ElementEnhanceStoneName = table.Column<int>(nullable: false),
                    ElementStonePlayerLevelNeed = table.Column<int>(nullable: false),
                    ElementStoneRarity = table.Column<int>(nullable: false),
                    ElementStoneValue = table.Column<int>(nullable: false),
                    ElementType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementEnhanceStoneDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicEnhanceStoneInfoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhysicAttackEnhance = table.Column<int>(nullable: false),
                    PhysicDefenceEnhance = table.Column<int>(nullable: false),
                    PhysicEnhanceStoneName = table.Column<int>(nullable: false),
                    PhysicStonePlayerLevelNeed = table.Column<int>(nullable: false),
                    PhysicStoneRarity = table.Column<int>(nullable: false),
                    PhysicStoneValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicEnhanceStoneInfoDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeaponDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElementAttackDefault = table.Column<int>(nullable: false),
                    ElementDefenceDefault = table.Column<int>(nullable: false),
                    ElementTypeDefault = table.Column<int>(nullable: false),
                    ExperienceDefault = table.Column<int>(nullable: false),
                    LevelDefault = table.Column<int>(nullable: false),
                    PhysicAttackDefault = table.Column<int>(nullable: false),
                    PhysicDefenceDefault = table.Column<int>(nullable: false),
                    RarityDefault = table.Column<int>(nullable: false),
                    SkillDefault = table.Column<int>(nullable: false),
                    UnbreakRecoverTimesDefault = table.Column<int>(nullable: false),
                    UnbreakValueDefaultDefault = table.Column<float>(nullable: false),
                    WeaponIntroduce = table.Column<string>(nullable: true),
                    WeaponName = table.Column<int>(nullable: false),
                    WeaponType = table.Column<int>(nullable: false),
                    WeaponValueDefault = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeaponUpdateDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BreakCost = table.Column<int>(nullable: false),
                    BreakLevel = table.Column<int>(nullable: false),
                    BreakMaterial = table.Column<int>(nullable: false),
                    WeaponDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponUpdateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeaponUpdateDetails_WeaponDetails_WeaponDetailId",
                        column: x => x.WeaponDetailId,
                        principalTable: "WeaponDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeaponUpdateDetails_WeaponDetailId",
                table: "WeaponUpdateDetails",
                column: "WeaponDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementEnhanceStoneDetails");

            migrationBuilder.DropTable(
                name: "PhysicEnhanceStoneInfoDetails");

            migrationBuilder.DropTable(
                name: "WeaponUpdateDetails");

            migrationBuilder.DropTable(
                name: "WeaponDetails");
        }
    }
}
