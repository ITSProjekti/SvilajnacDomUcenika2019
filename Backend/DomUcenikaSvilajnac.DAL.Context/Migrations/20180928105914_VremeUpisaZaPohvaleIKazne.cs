﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DomUcenikaSvilajnac.DAL.Context.Migrations
{
    public partial class VremeUpisaZaPohvaleIKazne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VremeUpisa",
                table: "Pohvale",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "VremeUpisa",
                table: "Kazne",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("insert into Kazne (Opis, BodoviKazne, UcenikId, VremeUpisa) values ('Izbacen sa casa', 3, (select top 1 Id from Ucenici), getdate())");

            migrationBuilder.Sql("insert into Pohvale (Opis, UcenikId, BodoviPohvale, VremeUpisa) values ('Ističe se', (select top 1 Id from Ucenici), 5, getdate())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VremeUpisa",
                table: "Pohvale");

            migrationBuilder.DropColumn(
                name: "VremeUpisa",
                table: "Kazne");
        }
    }
}
