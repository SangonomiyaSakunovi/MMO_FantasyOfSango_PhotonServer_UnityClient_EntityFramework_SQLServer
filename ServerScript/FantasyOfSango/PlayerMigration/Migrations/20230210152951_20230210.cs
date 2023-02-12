using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerMigration.Migrations
{
    public partial class _20230210 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WeaponName",
                table: "WeaponInfos",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<int>(
                name: "ElementAttack",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementDefence",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementEnhanceStone",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementType",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Introduce",
                table: "WeaponInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicAttack",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicDefence",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicEnhanceStone",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rarity",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Skill",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Unbreak",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "WeaponIndex",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WeaponInfos_PlayerId",
                table: "WeaponInfos",
                column: "PlayerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponInfos_Players_PlayerId",
                table: "WeaponInfos",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeaponInfos_Players_PlayerId",
                table: "WeaponInfos");

            migrationBuilder.DropIndex(
                name: "IX_WeaponInfos_PlayerId",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "ElementAttack",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "ElementDefence",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "ElementEnhanceStone",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "ElementType",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "Introduce",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "PhysicAttack",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "PhysicDefence",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "PhysicEnhanceStone",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "Rarity",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "Skill",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "Unbreak",
                table: "WeaponInfos");

            migrationBuilder.DropColumn(
                name: "WeaponIndex",
                table: "WeaponInfos");

            migrationBuilder.AlterColumn<string>(
                name: "WeaponName",
                table: "WeaponInfos",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
