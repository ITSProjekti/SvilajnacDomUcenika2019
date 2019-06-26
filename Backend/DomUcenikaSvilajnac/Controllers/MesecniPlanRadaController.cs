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
    [Route("api/MesecniPlanRada")]
    public class MesecniPlanRadaController : Controller
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Inicijalizacija instance klase DrzavaController i deklarisanje mappera i unitofwork-a.
        /// </summary>
        public MesecniPlanRadaController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Vraca listu svih drzava koje se trenutno nalaze u bazi.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<MesecniPlanRadaResource>> GetMesecniPlanRada()
        {
            var listaMesecniPlanRada = await UnitOfWork.MesecniPlanRada.GetAllAsync();
            return Mapper.Map<List<MesecniPlanRada>, List<MesecniPlanRadaResource>>(listaMesecniPlanRada.ToList());
        }

        /// <summary>
        /// Vraca jedan red iz tabele, tj. jednu drzavu na osnovu prosledjenog Id-a.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMesecniPlanRadaById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mesecniPlanRada = await UnitOfWork.MesecniPlanRada.GetAsync(id);
            var godisnjiProgramRadaNovi = Mapper.Map<MesecniPlanRada, MesecniPlanRadaResource>(mesecniPlanRada);
            if (mesecniPlanRada == null)
            {
                return NotFound();
            }

            return Ok(mesecniPlanRada);
        }
        /// <summary>
        /// Metoda za update, menja podatke u nekom redu u tabeli, tj. o nekoj drzavi na osnovu prosledjenog Id-a 
        /// i vraca podatke o drzavi koji su namenjeni za front.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesecniPlanRada([FromRoute] int id, [FromBody] MesecniPlanRadaResource mesecniPlanRada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stariMesecniPlanRada = await UnitOfWork.MesecniPlanRada.GetAsync(id);
            if (id != mesecniPlanRada.Id)
            {
                return BadRequest();
            }
            if (stariMesecniPlanRada == null)
                return NotFound();


            mesecniPlanRada.Id = id;
            Mapper.Map<MesecniPlanRadaResource, MesecniPlanRada>(mesecniPlanRada, stariMesecniPlanRada);
            await UnitOfWork.SaveChangesAsync();

            var noviMesecniPlanRada = await UnitOfWork.MesecniPlanRada.GetAsync(id);
            Mapper.Map<MesecniPlanRada, MesecniPlanRadaResource>(noviMesecniPlanRada);
            return Ok(mesecniPlanRada);
        }

        /// <summary>
        /// Dodavanje novog reda u tabeli, tj. nove drzave.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostMesecniPlanRada([FromBody] MesecniPlanRadaResource mesecniPlanRada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var noviMesecniPlanRada = Mapper.Map<MesecniPlanRadaResource, MesecniPlanRada>(mesecniPlanRada);

            UnitOfWork.MesecniPlanRada.Add(noviMesecniPlanRada);
            await UnitOfWork.SaveChangesAsync();

            mesecniPlanRada = Mapper.Map<MesecniPlanRada, MesecniPlanRadaResource>(noviMesecniPlanRada);

            return Ok(mesecniPlanRada);
        }
        /// <summary>
        /// Brisanje jednog reda iz tabele na osnvou prosledjenog Id-a, tj. brisanje odredjene drzave iz tabele.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMesecniPlanRada([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mesecniPlanRada = await UnitOfWork.MesecniPlanRada.GetAsync(id);
            if (mesecniPlanRada == null)
            {
                return NotFound();
            }

            var noviMesecniPlanRada = Mapper.Map<MesecniPlanRada, MesecniPlanRadaResource>(mesecniPlanRada);
            UnitOfWork.MesecniPlanRada.Remove(mesecniPlanRada);
            await UnitOfWork.SaveChangesAsync();

            return Ok(noviMesecniPlanRada);
        }
    }
}