using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomUcenikaSvilajnac.Common.Interfaces;
using DomUcenikaSvilajnac.Common.Models;
using DomUcenikaSvilajnac.Common.Models.ModelResources;
using Microsoft.AspNetCore.Mvc;

namespace DomUcenikaSvilajnac.Controllers
{
    [Produces("application/json")]
    [Route("api/Sobe")]
    public class SobaController : Controller
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Inicijalizacija instance klase DrzavaController i deklarisanje mappera i unitofwork-a.
        /// </summary>
        public SobaController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Vraca listu svih drzava koje se trenutno nalaze u bazi.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<SobaResource>> GetSobe()
        {
            var listaSoba = await UnitOfWork.Sobe.GetAllAsync();
            return Mapper.Map<List<Soba>, List<SobaResource>>(listaSoba.ToList());
        }

        /// <summary>
        /// Vraca jedan red iz tabele, tj. jednu drzavu na osnovu prosledjenog Id-a.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSobaById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var soba = await UnitOfWork.Sobe.GetAsync(id);
            var sobaNova = Mapper.Map<Soba, SobaResource>(soba);
            if (soba == null)
            {
                return NotFound();
            }

            return Ok(sobaNova);
        }
        /// <summary>
        /// Metoda za update, menja podatke u nekom redu u tabeli, tj. o nekoj drzavi na osnovu prosledjenog Id-a 
        /// i vraca podatke o drzavi koji su namenjeni za front.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoba([FromRoute] int id, [FromBody] SobaResource soba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staraSoba = await UnitOfWork.Sobe.GetAsync(id);
            if (id != staraSoba.Id)
            {
                return BadRequest();
            }
            if (staraSoba == null)
                return NotFound();


            soba.Id = id;
            Mapper.Map<SobaResource, Soba>(soba, staraSoba);
            await UnitOfWork.SaveChangesAsync();

            var novaSoba = await UnitOfWork.Sobe.GetAsync(id);
            Mapper.Map<Soba, SobaResource>(novaSoba);
            return Ok(soba);
        }

        /// <summary>
        /// Dodavanje novog reda u tabeli, tj. nove drzave.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostSoba([FromBody] SobaResource soba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var novaSoba = Mapper.Map<SobaResource, Soba>(soba);

            UnitOfWork.Sobe.Add(novaSoba);
            await UnitOfWork.SaveChangesAsync();

            soba = Mapper.Map<Soba, SobaResource>(novaSoba);

            return Ok(soba);
        }
        /// <summary>
        /// Brisanje jednog reda iz tabele na osnvou prosledjenog Id-a, tj. brisanje odredjene drzave iz tabele.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoba([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var soba = await UnitOfWork.Sobe.GetAsync(id);
            if (soba == null)
            {
                return NotFound();
            }

            var novaSoba = Mapper.Map<Soba, SobaResource>(soba);
            UnitOfWork.Sobe.Remove(soba);
            await UnitOfWork.SaveChangesAsync();

            return Ok(novaSoba);
        }
        }
}