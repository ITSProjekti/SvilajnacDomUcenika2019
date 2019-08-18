using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.DAL.Context;
using AutoMapper;
using DomUcenikaSvilajnac.ModelResources;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using System.Collections.ObjectModel;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using Syncfusion.Drawing;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using DomUcenikaSvilajnac.Common.Services;

namespace DomUcenikaSvilajnac.Controllers
{
    /// <summary>
    /// Klasa UcenikController koja implementira CRUD metode.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Ucenik")]
    public class UcenikController : Controller
    {
        public IMapper _mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        public UcenikContext context;

        public IConverter _converter;

        public IHostingEnvironment _hostingEnvironment;
        public ITransliterator _transliterator;

        /// <summary>
        /// Inicijalizacija instance klase UcenikController i deklarisanje mappera i unitofwork-a.
        /// </summary>
        public UcenikController(IMapper mapper, IUnitOfWork unitOfWork,IConverter converter, IHostingEnvironment hostingEnvironment,ITransliterator transliterator)
        {
            _converter = converter;
            _mapper = mapper;
            UnitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _transliterator = transliterator;
        }

        /// <summary>
        /// Vraca listu sviih ucenika, koji se trenutno nalaze u bazi.
        /// </summary>
        /// GET: api/Ucenik        
        [HttpGet]
        public async Task<IEnumerable<UcenikResource>> GetUcenika()
        {
            var pom = await UnitOfWork.Ucenici.podaciUcenika();
        //    await UnitOfWork.SaveChangesAsync();
            return pom;
        }

        /// <summary>
        /// Vraca jedan red iz tabele, tj. jednog ucenika na osnovu prosledjenog Id-a.
        /// </summary>
        // GET: api/Ucenik/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUcenik([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mapiranUcenik = UnitOfWork.Ucenici.podaciUcenikaById(id);


            var ucenik = await UnitOfWork.Ucenici.GetAsync(id);
            var ucenikNovi = _mapper.Map<Ucenik, UcenikResource>(ucenik);
            if (ucenik == null)
            {
                return NotFound();
            }

            return Ok(ucenikNovi);
        }


        [HttpGet("{StatusPrijaveID}")]
        [Route("status")]
        public async Task<IActionResult> GetUcenikByStatus ( int StatusPrijaveID)
        {

            if(!ModelState.IsValid)
            {
                BadRequest();
            }

            var uceniciPoStatusuPrijave = await UnitOfWork.Ucenici.podaciUcenikaByStatusPrijave(StatusPrijaveID);

            if(uceniciPoStatusuPrijave == null)
            {
                return NotFound();
            }

            return Ok(uceniciPoStatusuPrijave);
        }

        [HttpGet("{VaspitnaGrupaID}")]
        [Route("vaspitnaGrupa")]
        public async Task<IActionResult> GetUcenikByVaspitnaGrupa(int VaspitnaGrupaID)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var uceniciPoVaspitnojGrupi= await UnitOfWork.Ucenici.podaciUcenikaByVaspitnaGrupa(VaspitnaGrupaID);

            if(uceniciPoVaspitnojGrupi == null)
            {
                return NotFound();
            }

            return Ok(uceniciPoVaspitnojGrupi);
        }

        /// <summary>
        /// Metoda za update, menja podatke u nekom redu u tabeli, tj. o nekom uceniku na osnovu prosledjenog Id-a 
        /// i vraca podatke o uceniku koji su namenjeni za front.
        /// </summary>
        // PUT: api/Ucenik/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUcenik([FromRoute] int id, [FromBody] PutUcenikaResource ucenik)
        {

            try
            {

            
            PutRoditeljaResource roditeljResurs = new PutRoditeljaResource()
            {
                Id = ucenik.Roditelji.Id,
                IdMajke = ucenik.Roditelji.IdMajke,
                ImeMajke = ucenik.Roditelji.ImeMajke,
                PrezimeMajke = ucenik.Roditelji.PrezimeMajke,
                ImeOca = ucenik.Roditelji.ImeOca,
                PrezimeOca = ucenik.Roditelji.PrezimeOca,
                BrojTelefonaMajke = ucenik.Roditelji.BrojTelefonaMajke,
                BrojTelefonaOca = ucenik.Roditelji.BrojTelefonaOca,
                StrucnaSpremaMajkeId = ucenik.Roditelji.StrucnaSpremaMajkeId,
                StrucnaSpremaOcaId = ucenik.Roditelji.StrucnaSpremaOcaId,
                UcenikId = ucenik.Roditelji.UcenikId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // pozivanje metode za update broja ucenika u vaspitnoj grupi
            await UnitOfWork.VaspitneGrupe.updateBrojaUcenikaUVaspitnojGrupi();

            var stariUcenik = await UnitOfWork.Ucenici.GetAsync(id);
            int pom = stariUcenik.TelefonId;

            TelefonResource telefon = new TelefonResource { Id = pom, Mobilni = ucenik.Telefon.Mobilni, Kucni = ucenik.Telefon.Kucni };
            var stariTelefon = await UnitOfWork.Telefoni.GetAsync(telefon.Id);

            //koriscenje klase telefon kontrolera kako bih pozvao metodu put za taj objekat
            TelefonController telefonKontroler = new TelefonController(_mapper, UnitOfWork);
            StarateljController starateljKontroler = new StarateljController(_mapper, UnitOfWork);
            var starateljUcenika = await UnitOfWork.Staratelji.selektIdStarateljaUcenika(stariUcenik.Id);

            bool noviStarateljKrozPut = false;
            if (starateljUcenika == null && ucenik.Staratelji.Ime != "")
            {
                await starateljKontroler.PostStaratelj(ucenik.Staratelji);
                noviStarateljKrozPut = true;
            }

            await telefonKontroler.PutTelefon(telefon.Id, telefon);
            if (id != stariUcenik.Id)
            {
                return BadRequest();
            }
            if (stariUcenik == null)
                return NotFound();

            ucenik.Id = id;

            //u zavisnosti od tipa porodice, ako ucenik nema staratelja u bazi nece postojati ni jedan staratelj tog ucenika
            //ovo treba promeniti ako se budemo odlucili da arhiviramo sve tabele
            if ((ucenik.TipPorodice.Id == 4 || ucenik.TipPorodice.Id == 5) && starateljUcenika != null)
            {

                ucenik.Staratelji.UcenikId = stariUcenik.Id;
                await starateljKontroler.PutStaratelj(starateljUcenika.Id, ucenik.Staratelji);
            }
            else if (!noviStarateljKrozPut)
            {
                //ovaj deo treba ponovo pogledati ako zelimo arhivirati stare staratelje
                if (starateljUcenika !=null)
                     await starateljKontroler.DeleteStaratelj(starateljUcenika.Id);
                stariUcenik.Staratelji.Add(new Staratelj { Id = 0, Ime = "", Prezime = "", UcenikId = 0 });
                ucenik.Staratelji = null;
            }

            var novi = _mapper.Map<PutUcenikaResource, Ucenik>(ucenik, stariUcenik);
            novi.TelefonId = pom;
            await UnitOfWork.SaveChangesAsync();

            //kreiranje instance kontrolera roditelja
            RoditeljController roditeljKontroler = new RoditeljController(_mapper, UnitOfWork);
            await roditeljKontroler.PutRoditelj(novi.Id, roditeljResurs);

            var noviUcenik = await UnitOfWork.Ucenici.mapiranjeZaPutUcenika(id);
            if ((ucenik.TipPorodice.Id != 4 && ucenik.TipPorodice.Id != 5) && starateljUcenika != null)
            {
                await starateljKontroler.DeleteStaratelj(noviUcenik.Staratelji.Id);
            }
            // pozivanje metode za update broja ucenika u vaspitnoj grupi
            await UnitOfWork.VaspitneGrupe.updateBrojaUcenikaUVaspitnojGrupi();
                return Ok(noviUcenik);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return BadRequest();
        }

        /// <summary>
        /// Dodavanje novog reda u tabeli, tj. novog ucenika.
        /// </summary>
            // POST: api/Ucenik
        [HttpPost]
        public async Task<IActionResult> PostUcenik([FromBody] PostUcenikaResource ucenik)
        {
            //instanciranje objekta za telefon radi cuvanja u tabelu telefon
            // Telefon mobilni = new Telefon { Mobilni = ucenik.Telefon.Mobilni, Kucni = ucenik.Telefon.Kucni };
            List<Roditelj> roditelji = new List<Roditelj>();
            Roditelj otac = new Roditelj()
            {
                Ime = ucenik.Roditelji.ImeOca,
                Prezime = ucenik.Roditelji.PrezimeOca,
                BrojTelefona = ucenik.Roditelji.BrojTelefonaOca,
                StepenObrazovanjaId = ucenik.Roditelji.StrucnaSpremaOcaId,
            };

            Roditelj majka = new Roditelj()
            {
                Ime = ucenik.Roditelji.ImeMajke,
                Prezime = ucenik.Roditelji.PrezimeMajke,
                BrojTelefona = ucenik.Roditelji.BrojTelefonaMajke,
                StepenObrazovanjaId = ucenik.Roditelji.StrucnaSpremaMajkeId,
            };
           
           

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           /*  hardkodovan id vaspitne grupe kako bi se ucenik nakon posta dodao u difoltnu vaspitnu grupu
            jer ne moze vaspitna grupa da bude null zbog foreign key-a */
             
            var noviUcenik = _mapper.Map<PostUcenikaResource, Ucenik>(ucenik);
            noviUcenik.VremeUpisa = DateTime.Now;
            noviUcenik.VaspitnaGrupaId = 1;
            noviUcenik.StatusPrijaveId = 1;
            ucenik.Staratelji.UcenikId = noviUcenik.Id;
            

            //cuvanje u bazi
            UnitOfWork.Ucenici.Add(noviUcenik);
            await UnitOfWork.SaveChangesAsync();

            //provera da li je tip porodice koji zahteva da ucenik ima staratelja, ako jeste pozvace se staratelj kontroler i njegova post metoda
            if (noviUcenik.TipPorodiceId == 4 || noviUcenik.TipPorodiceId == 5)
            {
                StarateljController starateljKontroler = new StarateljController(_mapper, UnitOfWork);
                ucenik.Staratelji.UcenikId = noviUcenik.Id;
                await starateljKontroler.PostStaratelj(ucenik.Staratelji);
            }
            
            otac.UcenikId = noviUcenik.Id;
            majka.UcenikId = noviUcenik.Id;

            roditelji.Add(otac);
            roditelji.Add(majka);

            // pozivanje metode za update broja ucenika u vaspitnoj grupi
            await UnitOfWork.VaspitneGrupe.updateBrojaUcenikaUVaspitnojGrupi();

            UnitOfWork.Roditelji.AddRange(roditelji);
            UnitOfWork.SaveChanges();

            //proverava da li je tip porodice koji zahteva da ucenik ima staratelja
            if (noviUcenik.TipPorodiceId != 4 || noviUcenik.TipPorodiceId != 5)
                noviUcenik.Staratelji.Add(new Staratelj { Id = 0, Ime = "", Prezime = "", UcenikId = 0 });
            ucenik = _mapper.Map<Ucenik, PostUcenikaResource>(noviUcenik);

            var mapiranUcenik = await UnitOfWork.Ucenici.mapiranjeZaPostUcenika(ucenik);

         
            return Ok(mapiranUcenik);
        }

        /// <summary>
        /// Brisanje jednog reda iz tabele na osnvou prosledjenog Id-a, tj. brisanje odredjenog ucenika iz tabele.
        /// </summary>
        // DELETE: api/Ucenik/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUcenik([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ucenik = await UnitOfWork.Ucenici.GetAsync(id);


            if (ucenik.Staratelji.Count == 0)
                ucenik.Staratelji = new Collection<Staratelj>()
                {
                    new Staratelj()
                    {
                        Id = 0,
                        Ime = "",
                        Prezime ="",
                        UcenikId = 0
                    }
                };
          
            var noviUcenik = _mapper.Map<Ucenik, UcenikResource>(ucenik);
            var mapiranUcenik = await UnitOfWork.Ucenici.mapiranjeZaDeleteUcenika(noviUcenik);

            if (ucenik == null)
            {
                return NotFound();
            }

            var telefonUcenika = await UnitOfWork.Telefoni.GetAsync(ucenik.Telefon.Id);

            //brisanje u bazi
            UnitOfWork.Ucenici.Remove(ucenik);
            UnitOfWork.Telefoni.Remove(telefonUcenika);
            await UnitOfWork.SaveChangesAsync();

            // pozivanje metode za update broja ucenika u vaspitnoj grupi nnakon brisanja
            await UnitOfWork.VaspitneGrupe.updateBrojaUcenikaUVaspitnojGrupi();

            return Ok(mapiranUcenik);

        }

        [HttpGet]
        [Route("pdf")]
        public IActionResult GetPdf( string muski, string zenski, string pismo,string razred,string naslov )
        {



            var imeFajla = UnitOfWork.Ucenici.vratiNaslove(naslov) + " " + muski + " " + zenski + " " + razred;
            if (pismo == "c")
                _transliterator.Transliterate(imeFajla);

            imeFajla += ".pdf";
           

            var globalSettings = new GlobalSettings
            {
                
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Left=10, Right=10},
                DocumentTitle = "RangLista",
                Out = Path.Combine(_hostingEnvironment.WebRootPath,"PDF", imeFajla),
                DPI=100,
                
                
                

            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = UnitOfWork.Ucenici.htmlListaRangiranih(muski, zenski, pismo, razred,naslov),
                
                WebSettings = { DefaultEncoding = "utf-8" ,UserStyleSheet = Path.Combine( _hostingEnvironment.WebRootPath,"PDFStyles","PDFstyle.css")},
                HeaderSettings = { FontName = "Arial", FontSize = 9 },
                FooterSettings = { FontName = "Arial", FontSize = 9 },
                
            };


            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = {objectSettings}
            };

            _converter.Convert(pdf);

            return Ok("/Files/PDF/" +imeFajla);


        }

    /*    [HttpGet("/{brojMuskih}/{brojZenskih}/{bodovi}")]
        public IActionResult GetUcenik(int brojMuskih, int brojZenskih, int bodovi)
        {
            IEnumerable<Ucenik> lista = UnitOfWork.Ucenici.vratiPrimljeneUcenike(brojMuskih, brojZenskih, bodovi);
            UnitOfWork.SaveChanges();

            return Ok(lista);
        }*/
        /*
         
        [HttpPost]
        public IActionResult CreateDocument()
        {
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            string a = "Andjelija Mihajlovic 1997";
            //Draw the text
            graphics.DrawString(a, font, PdfBrushes.Black, new PointF(0, 0));

            //Saving the PDF to the MemoryStream
            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            //Set the position as '0'.
            stream.Position = 0;

            //Download the PDF document in the browser
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            fileStreamResult.FileDownloadName = "Sample.pdf";

            return fileStreamResult;
        }
        */
    }
}