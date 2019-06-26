﻿using AutoMapper;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using DomUcenikaSvilajnac.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomUcenikaSvilajnac.DAL.RepoPattern
{
    /// <summary>
    /// Nasledjuje genericku klasu Repository sa tipom Statistika i IStatistikaRepository interfejs
    /// Videti Repository i Statistika klasu i IStatistikaRepository interfejs radi dodatnog pojasnjena.
    /// </summary>
    public class StatistikaRepository : Repository<Statistika>, IStatistikaRepository
    {
        protected readonly UcenikContext _context;
        public IMapper Mapper { get; }
        public StatistikaRepository(UcenikContext context, IMapper mapper) : base(context)
        {
            _context = context;
            Mapper = mapper;
        }

        /// <summary>
        /// Get the context.
        /// </summary>
        public UcenikContext context
        {
            get { return context as UcenikContext; }
        }

        public IStatistikaRepository Statistike { get; set; }

        public async Task<IEnumerable<StatistikaResource>> podaciStatistike()
        {
            var statistika = await _context.Statistike.Include(vp => vp.VaspitnaGrupa).ToListAsync();

            return Mapper.Map<List<Statistika>, List<StatistikaResource>>(statistika);
        }

        public async Task<StatistikaResource> podaciStatistikeById(int id)
        {
            var statistika = await _context.Statistike
                .Include(vp => vp.VaspitnaGrupa)
                .SingleOrDefaultAsync(x => x.Id == id);

            return Mapper.Map<Statistika, StatistikaResource>(statistika);
        }

        public async Task<StatistikaResource> mapiranjeZaPostStatistike(StatistikaResource statistika)
        {
            var podaciStatistike = await _context.Statistike
                .Include(vp => vp.VaspitnaGrupa)
                .SingleOrDefaultAsync(x => x.Id == statistika.Id);

            return Mapper.Map<Statistika, StatistikaResource>(podaciStatistike);
        }

        public async Task<StatistikaResource> mapiranjeZaPutStatistike(int id)
        {
            var podaciStatistike = await _context.Statistike
                .Include(vp => vp.VaspitnaGrupa)
                .SingleOrDefaultAsync(x => x.Id == id);

            return Mapper.Map<Statistika, StatistikaResource>(podaciStatistike);
        }

        public async Task<StatistikaResource> mapiranjeZaDeleteStatistike(StatistikaResource statistika)
        {
            var podaciStatistike = await _context.Statistike
               .Include(vp => vp.VaspitnaGrupa)
               .SingleOrDefaultAsync(x => x.Id == statistika.Id);

            return Mapper.Map<Statistika, StatistikaResource>(podaciStatistike);
        }
        public async Task<IEnumerable<StatistikaResource>> uspehUcenikaPoVaspitnimGrupama()
        {

            /* linq koji vraca id-eve vaspitnih grupa u kojima se nalazi bar jedan ucenik, distinct sluzi
             * da nam vrati rezultat bez duplih id-eva */

            var grupeUcenika = await _context.Uceniks
                .Select(n => n.VaspitnaGrupaId)
                .Distinct().ToListAsync();


            /* linq koji vraca  redove iz statistike, onih vaspitnih grupa u kojima se nalazi bar jedan ucenik
             * koje smo prethodno selektovali sa gornjim upitom (grupeUcenika), time cemo biti sigurni da se ne selektuje
             * red u statistici cija vaspitna grupa jos nema ucenika u sebi    */

            var statistike = await _context.Statistike
                .Where(n=> grupeUcenika.Contains(n.VaspitnaGrupaId))
                .ToListAsync();

            /* linq koji vraca prosecni uspeh ucenika po vaspitnim grupama  */

            var uspehPoGrupama = _context.Uceniks
                .GroupBy(n => n.VaspitnaGrupaId)
                .Select(k => k.Average(p => p.PrethodniUspeh))
                .ToList();



            int i = 0;

            /* foreach-om prolazimo kroz sve selektovane redove iz tabele statistika
             * i propertiju UspehVaspitneGrupe dodeljuje prethodno izracunati prosek ucenika po vaspitnim grupama*/

            foreach (var item in statistike)
                item.UspehVaspitneGrupe = uspehPoGrupama[i++];
            
            return Mapper.Map<List<Statistika>, List<StatistikaResource>>(statistike);
        }

        public async Task<IEnumerable<StatistikaResource>> posecenostSastanaka()
        {

            var sastanciVaspitnihGrupa = await _context.Sastanci
                                                 .Select(n => n.VaspitnaGrupaId)
                                                 .Distinct().ToListAsync();



            var sastanci = await _context.Statistike
                .Where(n => sastanciVaspitnihGrupa.Contains(n.VaspitnaGrupaId))
                .ToListAsync();



            var posecenostPoGrupama = _context.Sastanci
                .GroupBy(n => n.VaspitnaGrupaId)
                .Select(k => k.Sum(o=> ((o.UkupanBrojPrisutnihUcenika - o.BrojPrisutnihUcenika) / Convert.ToSingle( k.Sum(n=> n.UkupanBrojPrisutnihUcenika))))*100f)
                .ToList();


            int i = 0;

         

            foreach (var item in sastanci)
            {
                posecenostPoGrupama[i] -= 100;
                posecenostPoGrupama[i] *= -1;
                item.Posecenost = posecenostPoGrupama[i++].ToString() + "%";

            }
            return Mapper.Map<List<Statistika>, List<StatistikaResource>>(sastanci);
        }


        public async Task<IEnumerable<StatistikaResource>> bodoviPohvalaUcenikaPoGrupama()
        {
            // linq koji vraca listu id-eva ucenika koji imaju bar jednu pohvalu
            var idUcenikaUPohvalama = await _context.Pohvale
                                    .Select(n => n.UcenikId)
                                    .Distinct()
                                    .ToListAsync();


            // linq koji vraca id-eve vaspitnih grupa onih ucenika koji imaju pohvale    
            var idVaspitnihGrupaUcenikaSaPohvalom = await _context.Uceniks
                                                    .Where(k => idUcenikaUPohvalama.Contains(k.Id))
                                                    .Select(n => n.VaspitnaGrupaId).Distinct()
                                                    .ToListAsync();

            var statistikeZaUpdate = await _context.Statistike
              .Where(n => idVaspitnihGrupaUcenikaSaPohvalom.Contains(n.VaspitnaGrupaId))
              .ToListAsync();

            var sveStatistike = await _context.Statistike
                                      .ToListAsync();


            //resetovanje statistike pre novog racunanja bodova pohvala ucenika po vaspitnim grupama
            foreach (var item in sveStatistike)
                item.BodoviPohvalaGrupa = 0;


            /* sumiranje bodova pohvala ucenika po vaspitnim grupama 
             */
            var bodoviPohvala = _context.Pohvale
                                  .Where(n => idUcenikaUPohvalama.Contains(n.UcenikId))
                                  .GroupBy(n => new { n.Ucenik.VaspitnaGrupaId })
                                  .Select(n => n.Sum( o=> o.BodoviPohvale))
                                  .ToList();

            int i = 0;

            /* foreach-om prolazimo kroz sve selektovane redove iz tabele statistika
             * i propertiju bodoviPohvalaGrupa dodeljuje prethodno izracunatu sumu bodova pohvala ucenika po vaspitnim grupama*/

            foreach (var item in statistikeZaUpdate)
                item.BodoviPohvalaGrupa = bodoviPohvala[i++];
            return Mapper.Map<List<Statistika>, List<StatistikaResource>>(statistikeZaUpdate);
        }
    }
}
