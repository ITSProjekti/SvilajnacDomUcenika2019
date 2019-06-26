﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DomUcenikaSvilajnac.DAL.Context.Migrations
{
    public partial class ModelTabeleDrzave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivDrzave = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.Id);
                });

            migrationBuilder.Sql("insert into Drzave (NazivDrzave) values (N'Avganistan'), (N'Azerbejdžan'), (N'Albanija'), " +
                "(N'Alžir'), (N'Angola'), (N'Andora'), (N'Antigva i Barbuda'), (N'Argentina'), (N'Australija'), (N'Austrija')," +
                "(N'Bangladeš'), (N'Barbados'), (N'Bahami'), (N'Bahrein'), (N'Belgija'), (N'Belize'), (N'Belorusija'), (N'Benin'), " +
                "(N'Bolivija'), (N'Bosna i Hercegovina'), (N'Bocvana'), (N'Brazil'), (N'Brunej'), (N'Bugarska'), (N'Burkina Faso'), " +
                "(N'Burundi'), (N'Butan'), (N'Vanuatu'), (N'Venecuela'), (N'Vijetnam'), (N'Gabon'), (N'Gambija'), (N'Gana'), (N'Gvajana'), " +
                "(N'Gvatemala'), (N'Gvineja'), (N'Gvineja Bisao'),  (N'Grenada'), (N'Gruzija'), (N'Grčka'), (N'Danska'), (N'Dominika'), " +
                "(N'Dominikanska Republika'), (N'Egipat'), (N'Ekvador'), (N'Ekvatorijalna Gvineja'), (N'Eritreja'), (N'Estonija'), " +
                "(N'Etiopija'), (N'Zambija'), (N'Zelenortska Ostrva'),(N'Zimbabve'), (N'Izrael'), (N'Inđija'), (N'Indonezija'), (N'Irak'), " +
                "(N'Iran'), (N'Irska'), (N'Island'), (N'Istočni Timor'), (N'Italija'), (N'Jamajka'), (N'Japan'), (N'Jemen'), (N'Jermenija')," +
                "(N'Jordan'), (N'Južna Krojea'), (N'Južni Sudan'), (N'Južnoafrička Republika'), (N'Kazahstan'), (N'Kambodža'), (N'Kamerun')," +
                "(N'Kanada'), (N'Katar'), (N'Kenija'), (N'Kina'), (N'Kipar'), (N'Kirgistan'), (N'Kiribati'), (N'Kolumbija'), (N'DR Konogo'), (N'Kongo')," +
                "(N'Komori'), (N'Kostarika'), (N'Kuba'), (N'Kuvajt'), (N'Laos'), (N'Lesoto'), (N'Letonija'), (N'Liban'), (N'Liberija'), (N'Libija'), " +
                "(N'Litvanija'), (N'Lihtenštajn'), (N'Luksemburg'), (N'Madagaskar'), (N'Mađarska'), (N'Makedonija'), (N'Malavi'), (N'Maldivi'), (N'Malezija'), " +
                "(N'Mali'), (N'Malta'), (N'Maroko'), (N'Maršalska Ostrva'), (N'Mauritanija'), (N'Mauricijus'), (N'Meksiko'), (N'Mikronezija'), (N'Mjanmar'), " +
                "(N'Mozambik'), (N'Moldavija'), (N'Monako'), (N'Mongolija'), (N'Namibija'), (N'Nauru'), (N'Nemačka'), (N'Nepal'), (N'Niger'), (N'Nigerija'), " +
                "(N'Nikaragva'), (N'Novi Zeland'), (N'Norveška'), (N'Obala Slonovače'), (N'Oman'), (N'Pakistan'), (N'Palau'), (N'Panama'), " +
                "(N'Papua Nova Gvineja'), (N'Paragvaj'), (N'Peru'), (N'Poljska'), (N'Portugalija'), (N'Ruanda'), (N'Rumunija'), (N'Rusija'), " +
                "(N'Salvador'), (N'Samoa'), (N'San Marino'), (N'Sao Tome i Prinsipe'), (N'Saudijska Arabija'), (N'Svaziland'), (N'Sveta Lucija'), " +
                "(N'Sveti Vinsent i Grenadini'), (N'Sent Kits i Nevis'), (N'Severna Koreja'), (N'Sejšeli'), (N'Senegal'), (N'Sijera Leone'), " +
                "(N'Singapur'), (N'Sirija'), (N'Sjedinjene Američke Države'), (N'Slovačka'), (N'Slovenija'), (N'Solomonska Ostrva'), (N'Somalija'), " +
                "(N'Srbija'), (N'Sudan'), (N'Surinam'), (N'Tajland'), (N'Tanzanija'), (N'Tadžikistan'), (N'Togo'), (N'Tonga'), (N'Trinidad i Tobago'), " +
                "(N'Tuvalu'), (N'Tunis'), (N'Turkmenistan'), (N'Turska'), (N'Uganda'), (N'Uzbekistan'), (N'Ujedinjeni Arapski Emirati'), (N'Ujedinjeno Kraljevstvo')," +
                "(N'Ukrajina'), (N'Urugvaj'), (N'Filipini'), (N'Finska'), (N'Fidži'), (N'Francuska'), (N'Haiti'), (N'Holandija'), (N'Honduras'), (N'Hrvatska'), " +
                "(N'Centralnoafrička Republika'), (N'Crna Gora'), (N'Čad'), (N'Češka'), (N'Čile'), (N'Džibuti'), (N'Švajcarska'), (N'Švedska'), (N'Španija'), " +
                "(N'Šri Lanka'), (N'Vatikan'), (N'Palestina')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drzave");
        }
    }
}
