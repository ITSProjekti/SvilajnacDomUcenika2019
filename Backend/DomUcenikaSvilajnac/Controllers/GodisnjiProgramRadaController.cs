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
    [Route("api/GodisnjiProgramRada")]
    public class GodisnjiProgramRadaController : Controller
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Inicijalizacija instance klase DrzavaController i deklarisanje mappera i unitofwork-a.
        /// </summary>
        public GodisnjiProgramRadaController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Vraca listu svih drzava koje se trenutno nalaze u bazi.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<GodisnjiProgramRadaResource>> GetGodisnjiProgramRada()
        {
            var listaGodisnjiProgramRada = await UnitOfWork.GodisnjiProgramRada.GetAllAsync();
            return Mapper.Map<List<GodisnjiProgramRada>, List<GodisnjiProgramRadaResource>>(listaGodisnjiProgramRada.ToList());
        }

        /// <summary>
        /// Vraca jedan red iz tabele, tj. jednu drzavu na osnovu prosledjenog Id-a.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGodisnjiProgramRadaById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var godisnjiProgramRada = await UnitOfWork.GodisnjiProgramRada.GetAsync(id);
            var godisnjiProgramRadaNovi = Mapper.Map<GodisnjiProgramRada, GodisnjiProgramRadaResource>(godisnjiProgramRada);
            if (godisnjiProgramRada == null)
            {
                return NotFound();
            }

            return Ok(godisnjiProgramRada);
        }
        /// <summary>
        /// Metoda za update, menja podatke u nekom redu u tabeli, tj. o nekoj drzavi na osnovu prosledjenog Id-a 
        /// i vraca podatke o drzavi koji su namenjeni za front.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGodisnjiProgramRada([FromRoute] int id, [FromBody] GodisnjiProgramRadaResource godisnjiProgramRada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stariGodisnjiProgramRada = await UnitOfWork.GodisnjiProgramRada.GetAsync(id);
            if (id != godisnjiProgramRada.Id)
            {
                return BadRequest();
            }
            if (stariGodisnjiProgramRada == null)
                return NotFound();


            godisnjiProgramRada.Id = id;
            Mapper.Map<GodisnjiProgramRadaResource, GodisnjiProgramRada>(godisnjiProgramRada, stariGodisnjiProgramRada);
            await UnitOfWork.SaveChangesAsync();

            var noviGodisnjiProgramRada = await UnitOfWork.GodisnjiProgramRada.GetAsync(id);
            Mapper.Map<GodisnjiProgramRada, GodisnjiProgramRadaResource>(noviGodisnjiProgramRada);
            return Ok(godisnjiProgramRada);
        }

        /// <summary>
        /// Dodavanje novog reda u tabeli, tj. nove drzave.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostGodisnjiProgramRada([FromBody] GodisnjiProgramRadaResource godisnji)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var noviGodisnji = Mapper.Map<GodisnjiProgramRadaResource, GodisnjiProgramRada>(godisnji);

            UnitOfWork.GodisnjiProgramRada.Add(noviGodisnji);
            await UnitOfWork.SaveChangesAsync();

            godisnji = Mapper.Map<GodisnjiProgramRada, GodisnjiProgramRadaResource>(noviGodisnji);

            return Ok(godisnji);
        }
        /// <summary>
        /// Brisanje jednog reda iz tabele na osnvou prosledjenog Id-a, tj. brisanje odredjene drzave iz tabele.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGodisnjiProgramRada([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var godisnjiProgramRada = await UnitOfWork.GodisnjiProgramRada.GetAsync(id);
            if (godisnjiProgramRada == null)
            {
                return NotFound();
            }

            var novaGodisnjiProgramRada = Mapper.Map<GodisnjiProgramRada, GodisnjiProgramRadaResource>(godisnjiProgramRada);
            UnitOfWork.GodisnjiProgramRada.Remove(godisnjiProgramRada);
            await UnitOfWork.SaveChangesAsync();

            return Ok(novaGodisnjiProgramRada);
        }
    }
}
