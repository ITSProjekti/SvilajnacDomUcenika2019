﻿using AutoMapper;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using DomUcenikaSvilajnac.DAL.Context;
using DomUcenikaSvilajnac.DAL.RepoPattern;
using DomUcenikaSvilajnac.ModelResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomUcenikaSvilajnac.DAL.RepoPattern
{
    /// <summary>
    /// Nasledjuje genericku klasu Repository sa tipom Ucenik i IUcenikRepository interfejs
    /// Videti Repository i Ucenik klasu i IUcenikRepository interfejs radi dodatnog pojasnjena.
    /// </summary>
    public class UcenikRepository : Repository<Ucenik>, IUcenikRepository
    {
        protected readonly UcenikContext _context;
        public IMapper Mapper { get; }
        /// <summary>
        /// Inicijalizacije instance klase UcenikRepository.
        /// </summary>
        public UcenikRepository(UcenikContext context, IMapper mapper) : base(context)
        {
            _context = context;
            Mapper = mapper;
        }

        public IUcenikRepository Ucenici { get; set; }
        /// <summary>
        /// Get the context.
        /// </summary>
        public UcenikContext context
        {
            get { return context as UcenikContext; }
        }

        public async Task<IEnumerable<UcenikResource>> podaciUcenika()
        {
            var podaciUcenika = await _context.Uceniks
                .Include(o => o.Opstina)
                .Include(d => d.DrzavaRodjenja)
                .Include(op => op.OpstinaPrebivalista)
                .Include(p => p.Pol)
                .Include(t => t.Telefon)
                .Include(pb => pb.PostanskiBroj)
                .Include(os => os.PrethodnaSkola)
                .Include(ss => ss.UpisanaSkola.Opstina)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
                .Include(mzs => mzs.MestoZavrseneSkole)
                .Include(s => s.Smer)
                .Include(r => r.Razred)
                .Include(rod => rod.Roditelji)
                .Include(tipP => tipP.TipPorodice)
                .Include(st => st.Staratelji)
                .Include(vg => vg.VaspitnaGrupa)
                .Include(sp => sp.StatusPrijave)
                .ToListAsync();


            foreach (var item in podaciUcenika)
            {
                if (item.Staratelji.Count == 0)
                    item.Staratelji = new Collection<Staratelj>() {
                        new Staratelj()
                        {
                            Id = 0,
                            Ime = "",
                            Prezime = "",
                            UcenikId = 0
                        }
                    };
            }




           /* foreach (var ucenik in podaciUcenika)
                if (ucenik.StatusPrijaveId == 1)
                    ucenik.BodoviZaUpis = formulaZaRangiranje(ucenik.Id);
            else if (ucenik.StatusPrijaveId == 3)
                ucenik.BodoviZaUpis = formulaZaRangiranje(ucenik.Id);
            else if(ucenik.StatusPrijaveId==4)
                    ucenik.BodoviZaUpis = 0;

            podaciUcenika.OrderByDescending(m => m.BodoviZaUpis);

            _context.UpdateRange(podaciUcenika);*/
            


            return Mapper.Map<List<Ucenik>, List<UcenikResource>>(podaciUcenika );

        }

        public async Task<UcenikResource> podaciUcenikaById(int id)
        {
            var podaciUcenikaById = await _context.Uceniks
                  .Include(o => o.Opstina)
                .Include(d => d.DrzavaRodjenja)
                .Include(op => op.OpstinaPrebivalista)
                .Include(p => p.Pol)
                .Include(t => t.Telefon)
                .Include(pb => pb.PostanskiBroj)
                .Include(os => os.PrethodnaSkola)
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
                .Include(mzs => mzs.MestoZavrseneSkole)
                .Include(s => s.Smer)
                .Include(r => r.Razred)
                .Include(rod => rod.Roditelji)
                .Include(tipP => tipP.TipPorodice)
                .Include(st => st.Staratelji)
                .Include(vg => vg.VaspitnaGrupa)
                .Include(sp => sp.StatusPrijave)
                .SingleOrDefaultAsync(x => x.Id == id);
            return Mapper.Map<Ucenik, UcenikResource>(podaciUcenikaById);
        }

        public async Task<IEnumerable<UcenikResource>> podaciUcenikaByStatusPrijave(int prijavaId)
        {
            var podaciUcenika = await _context.Uceniks
               .Include(o => o.Opstina)
               .Include(d => d.DrzavaRodjenja)
               .Include(op => op.OpstinaPrebivalista)
               .Include(p => p.Pol)
               .Include(t => t.Telefon)
               .Include(pb => pb.PostanskiBroj)
               .Include(os => os.PrethodnaSkola)
               .Include(ss => ss.UpisanaSkola.Opstina)
               .Include(mr => mr.MestoRodjenja)
               .Include(mr => mr.MestoPrebivalista)
               .Include(mzs => mzs.MestoZavrseneSkole)
               .Include(s => s.Smer)
               .Include(r => r.Razred)
               .Include(rod => rod.Roditelji)
               .Include(tipP => tipP.TipPorodice)
               .Include(st => st.Staratelji)
               .Include(vg => vg.VaspitnaGrupa)
               .Include(sp => sp.StatusPrijave)
               .ToListAsync();

                return Mapper.Map<List<Ucenik>, List<UcenikResource>>(podaciUcenika.Where(m=>m.StatusPrijave.Id == prijavaId).ToList() );
        }


       

        public async Task<PostUcenikaResource> mapiranjeZaPostUcenika(PostUcenikaResource ucenik)
        {
            var podaciUcenika = await _context.Uceniks
                .Include(o => o.Opstina)
                .Include(d => d.DrzavaRodjenja)
                .Include(op => op.OpstinaPrebivalista)
                .Include(p => p.Pol)
                .Include(t => t.Telefon)
                .Include(pb => pb.PostanskiBroj)
                .Include(os => os.PrethodnaSkola)
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
                .Include(mzs => mzs.MestoZavrseneSkole)
                .Include(s => s.Smer)
                .Include(r => r.Razred)
                .Include(tipP => tipP.TipPorodice)
                .Include(vg => vg.VaspitnaGrupa)
                .Include(sp => sp.StatusPrijave)
                .SingleOrDefaultAsync(x => x.Id == ucenik.Id);

            return Mapper.Map<Ucenik, PostUcenikaResource>(podaciUcenika);
        }

        public async Task<PutUcenikaResource> mapiranjeZaPutUcenika(int id)
        {
            var podaciUcenika = await _context.Uceniks
                .Include(o => o.Opstina)
                .Include(d => d.DrzavaRodjenja)
                .Include(op => op.OpstinaPrebivalista)
                .Include(p => p.Pol)
                .Include(t => t.Telefon)
                .Include(pb => pb.PostanskiBroj)
                .Include(os => os.PrethodnaSkola)
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
                .Include(mzs => mzs.MestoZavrseneSkole)
                .Include(s => s.Smer)
                .Include(r => r.Razred)
                .Include(rod => rod.Roditelji)
                .Include(tipP => tipP.TipPorodice)
                .Include(vg => vg.VaspitnaGrupa)
                .Include(sp => sp.StatusPrijave)
                .SingleOrDefaultAsync(x => x.Id == id);
            return Mapper.Map<Ucenik, PutUcenikaResource>(podaciUcenika);
        }

        public async Task<UcenikResource> mapiranjeZaDeleteUcenika(UcenikResource ucenik)
        {
            var podaciUcenika = await _context.Uceniks
                .Include(o => o.Opstina)
                .Include(d => d.DrzavaRodjenja)
                .Include(op => op.OpstinaPrebivalista)
                .Include(p => p.Pol)
                .Include(t => t.Telefon)
                .Include(pb => pb.PostanskiBroj)
                .Include(os => os.PrethodnaSkola)
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
                .Include(mzs => mzs.MestoZavrseneSkole)
                .Include(s => s.Smer)
                .Include(r => r.Razred)
                .Include(rod => rod.Roditelji)
                .Include(tipP => tipP.TipPorodice)
                .Include(st => st.Staratelji)
                .Include(vg => vg.VaspitnaGrupa)
                .Include(sp => sp.StatusPrijave)
                .SingleOrDefaultAsync(x => x.Id == ucenik.Id);

            return Mapper.Map<Ucenik, UcenikResource>(podaciUcenika);
        }

        public float formulaZaRangiranje(int idUcenika)
        {

            var ucenik = _context.Uceniks.SingleOrDefault(n => n.Id == idUcenika);
            float rezultat = 0;
            float sumaBodovaPohvala = _context.Pohvale.Where(o => o.UcenikId == idUcenika).Sum(n => n.BodoviPohvale);
            float sumaBodovaKazni = _context.Kazne.Where(o => o.UcenikId == idUcenika).Sum(n => n.BodoviKazne);

            rezultat =((ucenik.PrethodniUspeh * 7) + (sumaBodovaPohvala - sumaBodovaKazni))+(3*ucenik.BrojPutaUDomu);

            return Convert.ToSingle(Math.Round(rezultat,2));
        }



        public IEnumerable<UcenikResource> vratiPrimljeneUcenike(int brojMuskih, int brojZenskih, int bodovi)
        {
            List<Ucenik> primljeni = new List<Ucenik>();
            var ucenici = _context.Uceniks.OrderBy(n => n.BodoviZaUpis).ToArray();
            foreach (Ucenik item in ucenici)
            {
                item.BodoviZaUpis = formulaZaRangiranje(item.Id);
                if ((item.BodoviZaUpis >= bodovi) && (brojMuskih > 0 || brojZenskih > 0))
                {
                    if(item.PolId == 1) //Pol muski
                    {
                        item.StatusPrijaveId = 3; //primljen
                        primljeni.Add(item);
                        
                        brojMuskih--;
                    }
                    else if(item.PolId == 2) // Pol zenski
                    {
                        item.StatusPrijaveId = 3; //primljen
                        primljeni.Add(item);

                        brojZenskih--;
                    }
                    //else if(item.PolId == 3)
                    //{
                    //    primljeni.Add(item);
                    //}
                }
                else
                {
                    item.StatusPrijaveId = 4; //odbijen
                }
            }
            return Mapper.Map<List<Ucenik>,List<UcenikResource> >(primljeni); 
           
        }

        public IEnumerable<UcenikResource> vratiPrimljene()
        {
            return Mapper.Map<List<Ucenik>, List<UcenikResource>>(_context.Uceniks.Where(m => m.StatusPrijaveId == 3).ToList());
            
        }


        public  string htmlListaRangiranih()
        {
            try
            {
                var rangirani = _context.Uceniks.Where(m => m.StatusPrijaveId == 3).ToList();

                var sb = new StringBuilder();

                sb.Append(@"<html> <head> </head>
               <body>      
                    <h1> gerisani pdf </h1>
                    <table align='center'>
                       <tr> 
                        <th> Ime</th>
                        <th> Prezime</th>

                        </tr>

                    ");


                foreach (Ucenik u in rangirani)
                {
                    sb.AppendFormat(@"<tr>
                    <td>{0}</td>
                    <td>{1}</td>

                </tr>
                    


                        ", u.Ime, u.Prezime);
                }

                sb.Append(@"</table>
                    </body>
                    </html>");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
               
        }

        
    } 
}
