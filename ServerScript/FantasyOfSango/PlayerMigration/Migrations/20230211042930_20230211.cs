using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlayerMigration.Migrations
{
    public partial class _20230211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Introduce",
                table: "WeaponInfos");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "WeaponInfos",
                newName: "WeaponValue");

            migrationBuilder.AddColumn<int>(
                name: "WeaponType",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnhanceStoneInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ElementAttackEnhance = table.Column<int>(nullable: false),
                    ElementDefenceEnhance = table.Column<int>(nullable: false),
                    ElementEnhanceStoneName = table.Column<int>(nullable: false),
                    ElementPlayerLevelNeed = table.Column<int>(nullable: false),
                    ElementStoneRarity = table.Column<int>(nullable: false),
                    ElementStoneValue = table.Column<int>(nullable: false),
                    ElementType = table.Column<int>(nullable: false),
                    PhysicAttackEnhance = table.Column<int>(nullable: false),
                    PhysicDefenceEnhance = table.Column<int>(nullable: false),
                    PhysicEnhanceStoneName = table.Column<int>(nullable: false),
                    PhysicPlayerLevelNeed = table.Column<int>(nullable: false),
                    PhysicStoneRarity = table.Column<int>(nullable: false),
                    PhysicStoneValue = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnhanceStoneInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnhanceStoneInfos_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnhanceStoneInfos_PlayerId",
                table: "EnhanceStoneInfos",
                column: "PlayerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnhanceStoneInfos");

            migrationBuilder.DropColumn(
                name: "WeaponType",
                table: "WeaponInfos");

            migrationBuilder.RenameColumn(
                name: "WeaponValue",
                table: "WeaponInfos",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Introduce",
                table: "WeaponInfos",
                nullable: true);
        }
    }
}
