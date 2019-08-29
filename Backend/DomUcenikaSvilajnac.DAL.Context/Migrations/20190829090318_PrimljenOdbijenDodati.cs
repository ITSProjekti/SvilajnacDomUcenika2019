using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DomUcenikaSvilajnac.DAL.Context.Migrations
{
    public partial class PrimljenOdbijenDodati : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT dbo.StatusiPrijave ON;  
                                   INSERT [dbo].[StatusiPrijave] ([Id], [Status]) VALUES (3, N'Primljen')
                                   INSERT [dbo].[StatusiPrijave] ([Id], [Status]) VALUES (4, N'Odbijen')
                                    SET IDENTITY_INSERT dbo.StatusiPrijave OFF;  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
