using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DomUcenikaSvilajnac.DAL.Context.Migrations
{
    public partial class SmeroviDoma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Smerovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivSmera = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smerovi", x => x.Id);
                });

            migrationBuilder.Sql("insert into Smerovi (NazivSmera) values " +
               "('Aranžer u trgovini')," +
               "('Aranžer (umetničke škole)')," +
               "('Avio tehničar za elektronsku - elektro opremu vazduhoplova')," +
               "('Administrator računarskih mreža')," +
               "('Armirač - betonirac')," +
               "('Autoeletričar/Elektrotehničar za elektroniku na vozilima')," +
               "('Automehaničar')," +
               "('Arhitektonski tehničar')," +
               "('Aviotehničar/Aviotehničar za vazduhoplov i motor')," +
               "('Alatničar')," +
               "('Brodovođa')," +
               "('Bravar - mašinbravar')," +
               "('Bačvar')," +
               "('Birotehničar')," +
               "('Bilingvalni (dvojezični) program')," +
               "('Bankarski službenik')," +
               "('Brodomehaničar')," +
               "('Carinski tehničar')," +
               "('Cvećar - vrtlar')," +
               "('Dekorater zidnih površina')," +
               "('Dizajner proizvoda od kože / Tehničar-modelar kože / Galanterista')," +
               "('Dizajner zvuka')," +
               "('Drvorezbar')," +
               "('Dizajner tekstilnih materijala - tehničar dizajna tekstila')," +
               "('Ekonomski tehničar')," +
               "('Elektroinstalater / Električar')," +
               "('Elektromehaničar (tehničar) za termičke i rashladne uređaje')," +
               "('Elektrotehničar energetike')," +
               "('Elektromehaničar za mašine i opremu')," +
               "('Elektrotehničar procesnog upravljanja')," +
               "('Elektrotehničar informacionih tehnilogija / Elektrotehničar računara')," +
               "('Elektrotehničar telekomunikacija')," +
               "('Elektrotehničar signalno-sigurnosnih postrojenja')," +
               "('Elektromonter mreža i postrojenja')," +
               "('Elektrotehničar multimedija (radio i video tehnike)')," +
               "('Elektrotehničar elektronike')," +
               "('Elektrotehničar elektromotornih pogona')," +
               "('Farmaceutski tehničar')," +
               "('Firmopisac - kaligraf')," +
               "('Fotograf')," +
               "('Finansijski administrator (tehničar)')," +
               "('Fiziotarapeutski tehničar')," +
               "('Frizer muški i ženski')," +
               "('Geodetski tehničar - geometar')," +
               "('Geološki tehničar za istraživanje mineralnih sirovina')," +
               "('Građevinski tehničar hidrogradnje / Hidrograđevinar')," +
               "('Građevinski tehničar visokogradnje')," +
               "('Gumar')," +
               "('Graver')," +
               "('Geološki tehničar za geotehniku i hidrogeologiju')," +
               "('Ginekološko-akušerska sestra/tehničar')," +
               "('Gimnazija - Društveno jezički smer')," +
               "('Gimnazija opšteg tipa')," +
               "('Građevinski tehničar niskogradnje')," +
               "('Građevinski tehničar za laboratorijska ispitivanja / Građevinski laborant')," +
               "('Grnčar')," +
               "('Gimnazija Prirodno-matematički smer')," +
               "('Gimnazija za učenike sa posebnim sposobnostima')," +
               "('Hemijski laborant')," +
               "('Hidrološki tehničar')," +
               "('Hemijsko-tehnološki tehničar')," +
               "('Igrač')," +
               "('Izrađivač intarzija')," +
               "('Izvođač instalaterskih i završnih građevinskih radova')," +
               "('Izolatet - asfalter')," +
               "('Izrađivač mozaika')," +
               "('Izrađivač veza')," +
               "('Instalater')," +
               "('Izrađivač stare nošnje (terzija i abadžija)')," +
               "('Izrađivač hemijskih proizvoda')," +
               "('Informatički smer u gimnaziji')," +
               "('Juvelir umetničkih predmeta')," +
               "('Kamenorezac - klesar')," +
               "('Komercijalista / Transportni komercijalista')," +
               "('Konfekcijski tehničar / Konfekcionar kože i krzna')," +
               "('Konobar')," +
               "('Kozmetički tehničar / Kozmetoterapeut')," +
               "('Kuvar')," +
               "('Kazandžija')," +
               "('Keramičar, teracer, pećar')," +
               "('Kulinarski tehničar')," +
               "('Kovač')," +
               "('Kondukter u železničkom saobraćaju')," +
               "('Konfekcionar - krojač (šivač) / Konfekcionar tekstila')," +
               "('Konzervator kulturnih dobara')," +
               "('Krovopokrivač')," +
               "('Laboratorijski tehničar')," +
               "('Livac kalupar')," +
               "('Lakirer')," +
               "('Limar/autolimar')," +
               "('Livac umetničkih predmeta')," +
               "('Likovni tehničar')," +
               "('Manikir i pedikir')," +
               "('Maser')," +
               "('Mašinski tehničar motornih vozila')," +
               "('Mašinski tehničar za reparaturu')," +
               "('Mašinski tehničar merne i regulacione tehnike')," +
               "('Mašinski tehničar')," +
               "('Mašinski tehničar za kompjutersko kontruisanje')," +
               "('Medicinska sestra - tehničar')," +
               "('Metalostrugar')," +
               "('Medicinska sestra - vaspitač')," +
               "('Mehaničar hidroenergetskih sistema')," +
               "('Mehaničar grejne i rashladne tehnike')," +
               "('Mehaničar medicinske i laboratorijske opreme')," +
               "('Mehaničar privredne mehanizacije / Industrijski mehaničar')," +
               "('Mehatroničar za radarske sisteme')," +
               "('Mehatroničar za raketne sisteme')," +
               "('Mehatroničar za transportne sisteme aerodroma')," +
               "('Mehaničar tekstilnih mašina')," +
               "('Mesar')," +
               "('Metalobrusač')," +
               "('Mehaničar gaso i pneumoenergetskih sistema')," +
               "('Meteorološki tehničar')," +
               "('Monter suve gradnje')," +
               "('Monter telekomunikacionih mreža')," +
               "('Muzički izvođač (Vokalno-instrumentalni smer)')," +
               "('Muzički saradnik (Teoretski smer)')," +
               "('Nautički tehničar / Brodomašinski tehničar')," +
               "('Novinar saradnik')," +
               "('Obućar')," +
               "('Operater / Pogonski tehničar mašinske obrade')," +
               "('Opančar')," +
               "('Pedijatrijska sestra - tehničar')," +
               "('Pekar')," +
               "('Pivar')," +
               "('Plastičar umetničkih predmeta')," +
               "('Poljoprivredni proizvođač')," +
               "('Poljoprivredni tehničar')," +
               "('Poslastičar')," +
               "('Poslovni administrator')," +
               "('Pravni tehničar')," +
               "('Precizni mehaničar')," +
               "('Prehrambeni tehničar')," +
               "('Preparator zidnog i štafelajnog slikarstva')," +
               "('Prerađivač mleka')," +
               "('Prodavac - Trgovac')," +
               "('Proizvođač prehrambenih proizvoda')," +
               "('Pozlatar')," +
               "('Rasadničar')," +
               "('Rudarski tehničar')," +
               "('Rukovalac / Mehaničar poljoprivredne tehnike')," +
               "('Rukovalac građevinske mehanizacije')," +
               "('Rukovalac mehanizacijom u površinskoj eksploataciji')," +
               "('Rukovalac postrojenjima za dobijanje nafte i gasa')," +
               "('Sanitarno - ekološki tehničar')," +
               "('Saobraćajno-transportni tehničar')," +
               "('Saradnik u dramskoj umetnosti')," +
               "('Scenski masker i vlasuljar')," +
               "('Sitoštampar')," +
               "('Službenik osiguranja')," +
               "('Staklorezac')," +
               "('Stolar')," +
               "('Stilista saradnik')," +
               "('Stomatološka sestra - tehničar')," +
               "('Sarač')," +
               "('Stakloduvač')," +
               "('Trgovinski tehničar')," +
               "('Tehničar dizajna ambalaže')," +
               "('Tehničar / Mehaničar optike')," +
               "('Tehničar mašinske energetike')," +
               "('Tehničar za fitnes i zaštitu zdravlja')," +
               "('Tehničar dizajna grafike')," +
               "('Tehničar grafičke dorade')," +
               "('Tehničar mehatronike / Elektrotehničar automatike / Tehničar hidraulike i pneumatike')," +
               "('Tapetar dekorater')," +
               "('Tehničar za oblikovanje (dizajn) enterijera i industrijskih proizvoda (nameštaja)')," +
               "('Tehničar drumskog saobraćaja / Saobraćajno transportni tehničar')," +
               "('Tesar')," +
               "('Turistički tehničar')," +
               "('Tehničar za bezbednost saobraćaja')," +
               "('Tehničar za biotehnologiju')," +
               "('Tehničar za robotiku')," +
               "('Tehničar za zaštitu životne sredine')," +
               "('Tehničar za bezbednost vazdušnog saobraćaja')," +
               "('Tehničar zaštite od požara')," +
               "('Tekstilni tehničar / Šivač tekstila')," +
               "('Tehničar vazdušnog saobraćaja za spasavanje')," +
               "('Tehničar obezbeđenja')," +
               "('Tehničar za grafičku pripremu / Tehničar štampe')," +
               "('Tehničar modelar odeće / Dizajner odeće')," +
               "('Tehničar vuče')," +
               "('Tehničar za dobijanje metala')," +
               "('Tehničar za industrijsko - farmaceutsku tehnologiju')," +
               "('Tehničar za kompjutersko upravljanje')," +
               "('Tehničar za kozmetičku tehnologiju')," +
               "('Tehničar za lovstvo i ribolov')," +
               "('Tehničar za oblikovanje grafičkih prizvoda')," +
               "('Tehničar hortikulture')," +
               "('Tehničar za pejzažnu arhitekturu')," +
               "('Tehničar za reciklažu')," +
               "('Tehničar PTT saobraćaja')," +
               "('Tehničar unutrašnjeg transporta')," +
               "('Tehničar za održavanje objekata')," +
               "('Tehničar poljoprivredne tehnike')," +
               "('Ugostiteljski tehničar')," +
               "('Veterinarski tehničar')," +
               "('Vodoinstalater')," +
               "('Vojna gimnazija')," +
               "('Vozovođa')," +
               "('Vinogradar - Vinar')," +
               "('Vozač motornih vozila')," +
               "('Vitražista')," +
               "('Zavarivač')," +
               "('Zidar - Fasader')," +
               "('Zubni tehničar')," +
               "('Zdravstveni negovatelj')," +
               "('Zootehničar')," +
               "('Šumar')," +
               "('Šumarski tehničar')," +
               "('Ćilimar')," +
               "('Četkar')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Smerovi");
        }
    }
}
