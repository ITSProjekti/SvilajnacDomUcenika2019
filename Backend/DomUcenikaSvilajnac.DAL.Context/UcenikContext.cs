﻿using DomUcenikaSvilajnac.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomUcenikaSvilajnac.DAL.Context
{
    /// <summary>
    /// UcenikContext klasa koja nasledjuje DbContext koji predstavlja sesiju sa bazom podataka, 
    /// sluzi za upite tj querije i cuvanje podataka u bazi.
    /// Kombinacija Unit of work i Repository paterna
    /// </summary>
    public class UcenikContext : DbContext
    {
        /// <summary>
        /// Sluzi kao komunkacija sa bazom podataka.
        /// </summary>
        public DbSet<Ucenik> Uceniks { get; set; }
        public DbSet<Opstina> Opstine { get; set; }
        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Pol> Polovi { get; set; }
        public DbSet<Telefon> Telefoni { get; set; }
        public DbSet<PostanskiBroj> Brojevi { get; set; }
        public DbSet<PrethodnaSkola> PrethodneSkole { get; set; }
        public DbSet<UpisanaSkola> UpisaneSkole { get; set; }
        public DbSet<Mesto> Mesta { get; set; }
        public DbSet<Smer> Smerovi { get; set; }
        public DbSet<Razred> Razredi { get; set; }
        public DbSet<Roditelj> Roditelji { get; set; }
        public DbSet<StepenStrucneSpreme> Stepeni { get; set; }
        public DbSet<TipPorodice> TipoviPorodice { get; set; }
        public DbSet<Staratelj> Staratelji { get; set; }
        public DbSet<Pohvala> Pohvale { get; set; }
        public DbSet<VaspitnaGrupa> VaspitneGrupe { get; set; }
        public DbSet<Kazna> Kazne { get; set; }
        public DbSet<Vaspitac> Vaspitaci { get; set; }
        public DbSet<Sastanak> Sastanci { get; set; }
        public DbSet<Statistika> Statistike { get; set; }
        public DbSet<Soba> Sobe { get; set; }

        public DbSet<StatusPrijave> StatusiPrijave { get; set; }

        public DbSet<GodisnjiProgramRada> GodisnjiProgramiRada { get; set; }
        public DbSet<MesecniPlanRada> MesecniPlanoviRada { get; set; }

        /// <summary>
        /// Inicijalizuje se instaca UcenikContext klase.
        /// </summary>
        public UcenikContext(DbContextOptions<UcenikContext> options) : base(options)
        {

        }

      

    }
}
