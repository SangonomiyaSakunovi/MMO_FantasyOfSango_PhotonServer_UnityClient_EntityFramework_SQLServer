using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemMigration.Migrations
{
    public partial class _20230212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnbreakValueDefaultDefault",
                table: "WeaponDetails",
                newName: "UnbreakValueDefault");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnbreakValueDefault",
                table: "WeaponDetails",
                newName: "UnbreakValueDefaultDefault");
        }
    }
}
