using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerMigration.Migrations
{
    public partial class _20230211new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElementPlayerLevelNeed",
                table: "EnhanceStoneInfos");

            migrationBuilder.DropColumn(
                name: "ElementStoneValue",
                table: "EnhanceStoneInfos");

            migrationBuilder.DropColumn(
                name: "PhysicPlayerLevelNeed",
                table: "EnhanceStoneInfos");

            migrationBuilder.DropColumn(
                name: "PhysicStoneValue",
                table: "EnhanceStoneInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElementPlayerLevelNeed",
                table: "EnhanceStoneInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ElementStoneValue",
                table: "EnhanceStoneInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicPlayerLevelNeed",
                table: "EnhanceStoneInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicStoneValue",
                table: "EnhanceStoneInfos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
