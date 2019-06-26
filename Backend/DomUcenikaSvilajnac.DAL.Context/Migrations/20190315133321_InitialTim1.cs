using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DomUcenikaSvilajnac.DAL.Context.Migrations
{
    public partial class InitialTim1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ucenici_Mesta_MestoPrebivalistaId",
                table: "Ucenici");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucenici_Mesta_MestoRodjenjaId",
                table: "Ucenici");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucenici_Mesta_MestoZavrseneSkoleId",
                table: "Ucenici");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucenici_Opstine_OpstinaId",
                table: "Ucenici");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucenici_Opstine_OpstinaPrebivalistaId",
                table: "Ucenici");

            migrationBuilder.AlterColumn<int>(
                name: "OpstinaPrebivalistaId",
                table: "Ucenici",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OpstinaId",
                table: "Ucenici",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MestoZavrseneSkoleId",
                table: "Ucenici",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MestoRodjenjaId",
                table: "Ucenici",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MestoPrebivalistaId",
                table: "Ucenici",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ucenici_Mesta_MestoPrebivalistaId",
                table: "Ucenici",
                column: "MestoPrebivalistaId",
                principalTable: "Mesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucenici_Mesta_MestoRodjenjaId",
                table: "Ucenici",
                column: "MestoRodjenjaId",
                principalTable: "Mesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucenici_Mesta_MestoZavrseneSkoleId",
                table: "Ucenici",
                column: "MestoZavrseneSkoleId",
                principalTable: "Mesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucenici_Opstine_OpstinaId",
                table: "Ucenici",
                column: "OpstinaId",
                principalTable: "Opstine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucenici_Opstine_OpstinaPrebivalistaId",
                table: "Ucenici",
                column: "OpstinaPrebivalistaId",
                principalTable: "Opstine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
