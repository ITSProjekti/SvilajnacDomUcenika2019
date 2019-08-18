using AutoMapper;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using DomUcenikaSvilajnac.Common.Services;
using DomUcenikaSvilajnac.DAL.Context;
using DomUcenikaSvilajnac.ModelResources;
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
        public ITransliterator transliterator;
        /// <summary>
        /// Inicijalizacije instance klase UcenikRepository.
        /// </summary>
        public UcenikRepository(UcenikContext context, IMapper mapper) : base(context)
        {
            transliterator = LatUCirTransliterator.Instance;
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
                .Include(ss => ss.UpisanaSkola.Opstina)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
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
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
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
               .Include(ss => ss.UpisanaSkola.Opstina)
               .Include(mr => mr.MestoRodjenja)
               .Include(mr => mr.MestoPrebivalista)
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


        public async Task<IEnumerable<UcenikResource>> podaciUcenikaByVaspitnaGrupa(int vaspitnaGrupaId)
        {
            var podaciUcenika = await _context.Uceniks
               .Include(o => o.Opstina)
               .Include(d => d.DrzavaRodjenja)
               .Include(op => op.OpstinaPrebivalista)
               .Include(p => p.Pol)
               .Include(t => t.Telefon)
               .Include(pb => pb.PostanskiBroj)
               .Include(ss => ss.UpisanaSkola.Opstina)
               .Include(mr => mr.MestoRodjenja)
               .Include(mr => mr.MestoPrebivalista)
               .Include(s => s.Smer)
               .Include(r => r.Razred)
               .Include(rod => rod.Roditelji)
               .Include(tipP => tipP.TipPorodice)
               .Include(st => st.Staratelji)
               .Include(vg => vg.VaspitnaGrupa)
               .Include(sp => sp.StatusPrijave)
               .ToListAsync();

            return Mapper.Map<List<Ucenik>, List<UcenikResource>>(podaciUcenika.Where(m => m.VaspitnaGrupaId == vaspitnaGrupaId).ToList());
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
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
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
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
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
                .Include(ss => ss.UpisanaSkola)
                .Include(mr => mr.MestoRodjenja)
                .Include(mr => mr.MestoPrebivalista)
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


            if(ucenik.BrojPutaUDomu > 0)
            rezultat =((ucenik.PrethodniUspeh * 7) + (sumaBodovaPohvala - sumaBodovaKazni))+(3) + ucenik.MaterijalniPrihodi;
            else
            rezultat = ((ucenik.PrethodniUspeh * 7) + (sumaBodovaPohvala - sumaBodovaKazni)) +ucenik.MaterijalniPrihodi;


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
                    if(item.PolId == 1 && brojMuskih > 0) //Pol muski
                    {
                        item.StatusPrijaveId = 3; //primljen
                        primljeni.Add(item);
                        
                        brojMuskih--;
                    }
                    else if(item.PolId == 2 && brojZenskih >0) // Pol zenski
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
            return Mapper.Map<List<Ucenik>, List<UcenikResource>>(vratiPrimljeneUcenike().ToList());
            
        }
        

        /* ---------------------------- */

        public IQueryable<Ucenik> vratiPrimljeneUcenike()
        {
            return _context.Uceniks.Where(m => m.StatusPrijaveId == 3);
        }


        public IQueryable<Ucenik> vratiPoPolu(string pol, IQueryable<Ucenik> prethodni = null)
        {
            string[] polovi = pol.Split(",");

            if (polovi.Length == 1)
            {
                if (prethodni == null)
                    return _context.Uceniks.Where(m => m.Pol.NazivPola == pol)
                        .Include(m => m.Pol)
                        .Include(m => m.Razred);
                else
                    return prethodni.Where(m => m.Pol.NazivPola == pol)
                   .Include(m => m.Pol)
                   .Include(m => m.Razred);

            }
            else
            {
                if (prethodni == null)
                    return _context.Uceniks
                        .Include(m => m.Pol)
                        .Include(m => m.Razred);
                else
                    return prethodni
                   .Include(m => m.Pol)
                   .Include(m => m.Razred);
            }

        }

        public IQueryable<Ucenik> vratiPoRazredu(string Razred, IQueryable<Ucenik> prethodni = null)
        {

           

            if (Razred.Length == 1)
            {
                if (prethodni == null)
                    return _context.Uceniks.Where(m => m.Razred.BrojRazreda == Razred)
                    .Include(m => m.Pol)
                    .Include(m => m.Razred);
                else return prethodni.Where(m => m.RazredId == Convert.ToInt32(Razred))
                   .Include(m => m.Pol)
                   .Include(m => m.Razred);
            }

            else
            {
                char[] razred = Razred.ToCharArray();
                int[] razredId = new  int[razred.Length];
                int i = 0;
                foreach (var item in razred)
                {
                    razredId[i] = Convert.ToInt32(Char.GetNumericValue(item) );
                    i++;
                }

            


                if (prethodni == null)
                    return _context.Uceniks.Where(m=>razredId.Contains(m.RazredId))
                    .Include(m => m.Pol)
                    .Include(m => m.Razred);
                else return prethodni.Where(m =>razredId.Contains(m.RazredId) )
                   .Include(m => m.Pol)
                   .Include(m => m.Razred);


            }


        }


        public string vratiNaslove (string naslov)
        {

            string povratno;

            switch (naslov)
            {
                case "rang":
                    povratno = "Rang lista";
                    break;
                case "preliminarna":
                    povratno = "Preliminarna rang lista";
                    break;
                case "finalna":
                    povratno = "Finalna rang lista";
                    break;
                case "revidirana":
                    povratno = "Revidirana konacna rang lista";
                    break;

                default:
                    povratno = "Rang lista";
                    break;
               
            }

            return povratno;
        }















        //treba odvojiti vrati muske i vrati zenske
        public  string htmlListaRangiranih(string muski, string zenski, string pismo,string razred,string naslov)
        {
            try
            {
                var rangirani = new List<Ucenik>();

                string pol =muski +","+ zenski;



                IQueryable<Ucenik> uceniks = vratiPrimljeneUcenike();
                uceniks = vratiPoPolu(pol, uceniks);
                uceniks = vratiPoRazredu(razred, uceniks);    

                rangirani = uceniks.ToList();



                string listaNaslov = vratiNaslove(naslov);

                listaNaslov += " " + pol;




                var sb = new StringBuilder();

                if(pismo == "l")
                {
                    sb.Append(@"<html> <head> </head>
               <body>      
                    <h1> "+listaNaslov+ @" učenici </h1>
                    <table align='center'>
                       <tr align='center'>
                        <th>Redni broj</th>
                        <th>Ime</th>
                        <th>Prezime</th>
                        <th>Broj poena</th>
                        <th>Pol</th>
                        <th>Razred</th>

                        

                        </tr>

                    ");

                    int i = 1;
                    foreach (Ucenik u in rangirani)
                    {
                        sb.AppendFormat(@"<tr align='center'>
                    <td>{0}</td>
                    <td>{1}</td>
                    <td>{2}</td>
                    <td>{3}</td>
                    <td>{4}</td>
                    <td>{5}</td>



                </tr>
                    


                        ", i + ".", u.Ime, u.Prezime, u.BodoviZaUpis, u.Pol.NazivPola, u.Razred.BrojRazreda);

                        i++;
                    }

                    sb.Append(@"</table>
                    </body>
                    </html>");

                    return sb.ToString();
                }
                else
                {
                                        sb.Append(@"<html> <head> </head>
               <body>      
                    <h1> " + transliterator.Transliterate( listaNaslov)+  @" ученици </h1>
                    <table align='center'>
                       <tr align='center'>
                        <th>Редни број</th>
                        <th>Име</th>
                        <th>Презиме</th>
                        <th>Број поена</th>
                        <th>Пол</th>
                        <th>Разред</th>

                        

                        </tr>

                    ");

                    int i = 1;
                    foreach (Ucenik u in rangirani)
                    {
                        sb.AppendFormat(@"<tr align='center'>
                    <td>{0}</td>
                    <td>{1}</td>
                    <td>{2}</td>
                    <td>{3}</td>
                    <td>{4}</td>
                    <td>{5}</td>



                </tr>
                    


                        ", i + ".", transliterator.Transliterate(u.Ime), transliterator.Transliterate(u.Prezime), u.BodoviZaUpis, transliterator.Transliterate(u.Pol.NazivPola), u.Razred.BrojRazreda);

                        i++;
                    }

                    sb.Append(@"</table>
                    </body>
                    </html>");

                    return sb.ToString();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
               
        }

        
    } 
}
