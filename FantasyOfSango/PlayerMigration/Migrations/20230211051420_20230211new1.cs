using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerMigration.Migrations
{
    public partial class _20230211new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnBreakRecoverTimes",
                table: "WeaponInfos",
                newName: "UnbreakRecoverTimes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnbreakRecoverTimes",
                table: "WeaponInfos",
                newName: "UnBreakRecoverTimes");
        }
    }
}
