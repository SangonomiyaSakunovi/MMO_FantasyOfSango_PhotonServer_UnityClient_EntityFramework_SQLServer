using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerMigration.Migrations
{
    public partial class _20230211new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unbreak",
                table: "WeaponInfos",
                newName: "UnbreakValue");

            migrationBuilder.AddColumn<int>(
                name: "UnBreakRecoverTimes",
                table: "WeaponInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnBreakRecoverTimes",
                table: "WeaponInfos");

            migrationBuilder.RenameColumn(
                name: "UnbreakValue",
                table: "WeaponInfos",
                newName: "Unbreak");
        }
    }
}
